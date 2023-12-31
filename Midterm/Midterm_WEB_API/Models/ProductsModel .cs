﻿using Midterm_WEB_API.EF;
using Midterm_WEB_API.Reusables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Midterm_WEB_API.Models
{
    public class ProductsModel
    {
        #region Methods
        public ProductsResponse GetProducts()
        {
            var Result = new ProductsResponse();

            using (var db = new SimpleShopDBEntities())
            {
                Result.Products = db.Products.Select(i => new ProductDTO
                {
                    ID = i.ID,
                    Name = i.Name,
                    ExpireDate = i.ExpireDate,
                    DateAdded = i.DateAdded,

                }).ToList();
                Result.IsSuccess = true;
            }

            return Result;
        }

        public ProductResponse GetSingleProduct(int? ProductID)
        {
            var Result = new ProductResponse();

            using (var db = new SimpleShopDBEntities())
            {
                var DBItem = db.Products.FirstOrDefault(i => i.ID == ProductID);
                if (DBItem != null)
                {
                    Result.Product = new ProductDTO
                    {
                        ID = DBItem.ID,
                        Name = DBItem.Name,
                        ExpireDate = DBItem.ExpireDate,
                        DateAdded = DBItem.DateAdded

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

        public ApiResponseBase AddProduct(ProductDTO SubmitModel)
        {
            var Result = new ApiResponseBase();
            if (SubmitModel != null)
            {
                using (var db = new SimpleShopDBEntities())
                {
                    Product Product = new Product
                    {
                        Name = SubmitModel.Name,
                        ExpireDate = SubmitModel.ExpireDate,
                        DateAdded = SubmitModel.DateAdded ?? DateTime.Now
                    };
                    db.Products.Add(Product);
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

        public ApiResponseBase UpdateProduct(ProductDTO SubmitModel)
        {
            var Result = new ApiResponseBase();

            if (SubmitModel != null)
            {
                using (var db = new SimpleShopDBEntities())
                {
                    if (SubmitModel.ID != null && !db.Products.Any(i => i.ID == SubmitModel.ID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var ProductToUpdate = db.Products.Where(i => i.ID == SubmitModel.ID).First();

                        ProductToUpdate.Name = SubmitModel.Name;
                        ProductToUpdate.ExpireDate = SubmitModel.ExpireDate;
                        ProductToUpdate.DateAdded = SubmitModel.DateAdded;
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }

        public ApiResponseBase DeleteProduct(int? ProductID)
        {
            var Result = new ApiResponseBase();

            if (ProductID != null)
            {
                using (var db = new SimpleShopDBEntities())
                {
                    if (!db.Products.Any(i => i.ID == ProductID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var r = db.Products.Where(i => i.ID == ProductID).First();
                        db.Products.Remove(r);
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }
        #endregion

        #region Sub Classes
        public class ProductDTO
        {
            public int? ID { get; set; }
            public string Name { get; set; }
            public string City { get; set; }
            public DateTime? ExpireDate { get; set; }
            public DateTime? DateAdded { get; set; }
        }

        public class ProductsResponse : ApiResponseBase
        {
            public List<ProductDTO> Products { get; set; }
        }

        public class ProductResponse : ApiResponseBase
        {
            public ProductDTO Product { get; set; }
        }
        #endregion
    }
}