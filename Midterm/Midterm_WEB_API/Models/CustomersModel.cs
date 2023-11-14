using Midterm_WEB_API.EF;
using Midterm_WEB_API.Reusables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Midterm_WEB_API.Models
{
    public class CustomersModel
    {
        #region Methods
        public CustomersResponse GetCustomers()
        {
            var Result = new CustomersResponse();

            using (var db = new SimpleShopDBEntities())
            {
                Result.Customers = db.Customers.Select(i => new CustomerDTO
                {
                    ID = i.ID,
                    Name = i.Name,
                    City = i.City

                }).ToList();
                Result.IsSuccess = true;
            }

            return Result;
        }

        public CustomerResponse GetSingleCustomer(int? CustomerID)
        {
            var Result = new CustomerResponse();

            using (var db = new SimpleShopDBEntities())
            {
                var DBItem = db.Customers.FirstOrDefault(i => i.ID == CustomerID);
                if (DBItem != null)
                {
                    Result.Customer = new CustomerDTO
                    {
                        ID = DBItem.ID,
                        Name = DBItem.Name,
                        City = DBItem.City

                    };
                    Result.IsSuccess = true;
                }
                else
                {
                    Result.IsSuccess = false;
                    Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                    Result.StatusCode = HttpStatusCode.NotFound;
                }
            }

            return Result;
        }

        public ApiResponseBase AddCustomer(CustomerDTO SubmitModel)
        {
            var Result = new ApiResponseBase();
            if (SubmitModel != null)
            {
                using (var db = new SimpleShopDBEntities())
                {
                    Customer Customer = new Customer
                    {
                        Name = SubmitModel.Name,
                        City = SubmitModel.City
                    };
                    db.Customers.Add(Customer);
                    db.SaveChanges();
                    Result.IsSuccess = true;
                }
            }
            else
            {
                Result.IsSuccess = false;
                Result.ErrorMessage = Constants.ApiResponseMessages.NoDataGiven;
                Result.StatusCode = HttpStatusCode.BadRequest;
            }
            return Result;
        }

        public ApiResponseBase UpdateCustomer(CustomerDTO SubmitModel)
        {
            var Result = new ApiResponseBase();

            if (SubmitModel != null)
            {
                using (var db = new SimpleShopDBEntities())
                {
                    if (SubmitModel.ID != null && !db.Customers.Any(i => i.ID == SubmitModel.ID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var CustomerToUpdate = db.Customers.Where(i => i.ID == SubmitModel.ID).First();

                        CustomerToUpdate.Name = SubmitModel.Name;
                        CustomerToUpdate.City = SubmitModel.City;
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }

        public ApiResponseBase DeleteCustomer(int? CustomerID)
        {
            var Result = new ApiResponseBase();

            if (CustomerID != null)
            {
                using (var db = new SimpleShopDBEntities())
                {
                    if (!db.Customers.Any(i => i.ID == CustomerID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var r = db.Customers.Where(i => i.ID == CustomerID).First();
                        db.Customers.Remove(r);
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }
        #endregion

        #region Sub Classes
        public class CustomerDTO
        {
            public int? ID { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
        }

        public class CustomersResponse : ApiResponseBase
        {
            public List<CustomerDTO> Customers { get; set; }
        }

        public class CustomerResponse : ApiResponseBase
        {
            public CustomerDTO Customer { get; set; }
        }
        #endregion
    }
}