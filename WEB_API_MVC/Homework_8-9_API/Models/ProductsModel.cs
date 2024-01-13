using Homework_8_9_API.EF;
using Homework_8_9_API.Reusables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;

namespace Homework_8_9_API.Models
{
    public class ProductsModel
    {
        #region Methods
        public ProductsResponse GetProducts(int? CategoryID, int? CategoryParentID)
        {
            var Result = new ProductsResponse();

            using (var db = new SimpleShopDBModel())
            {
                if (CategoryID != null || CategoryParentID != null)
                {
                    var Categories = db.ProductCategories.Where(c => c.ProductCategoryID == CategoryID || c.ProductCategoryParentID == CategoryParentID).ToList();
                    Result.Products = new List<ProductDTO>();
                    foreach (var Cat in Categories)
                    {
                        Result.Products.AddRange(Cat.Products.Where(Item => !Result.Products.Any(i => Item.ID == i.ID)).Select(i => new ProductDTO
                        {
                            ID = i.ID,
                            Name = i.Name,
                            Description = i.Description,
                            Price = i.Price,
                            ExpireDate = i.ExpireDate,
                            DateAdded = i.DateAdded
                        }).ToList());
                    }
                }
                else {

                    Result.Products = db.Products.Select(i => new ProductDTO
                    {
                        ID = i.ID,
                        Name = i.Name,
                        Description = i.Description,
                        Price = i.Price,
                        ExpireDate = i.ExpireDate,
                        DateAdded = i.DateAdded
                    }).ToList();
                }
                Result.IsSuccess = true;
            }

            return Result;
        }

        public ProductResponse GetSingleProduct(int? ProductID)
        {
            var Result = new ProductResponse();

            using (var db = new SimpleShopDBModel())
            {
                var DBItem = db.Products.FirstOrDefault(i => i.ID == ProductID);
                if (DBItem != null)
                {
                    Result.Product = new ProductDTO
                    {
                        ID = DBItem.ID,
                        Name = DBItem.Name,
                        Description = DBItem.Description,
                        Price = DBItem.Price,
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
                using (var db = new SimpleShopDBModel())
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

        public ApiResponseBase UpdateProduct(ProductDTO SubmitModel, string UserEmail)
        {
            var Result = new ApiResponseBase();

            if (SubmitModel != null)
            {
                using (var db = new SimpleShopDBModel())
                {
                    if (SubmitModel.ID != null && !db.Products.Any(i => i.ID == SubmitModel.ID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var ProductToUpdate = db.Products.Where(i => i.ID == SubmitModel.ID).First();
                        StringBuilder ProductUpdateLogData = new StringBuilder();
                        if (SubmitModel.Name != null)
                        {
                            ProductUpdateLogData.AppendLine($"{ProductToUpdate.Name} => {SubmitModel.Name}");
                            ProductToUpdate.Name = SubmitModel.Name;
                        }
                        if (SubmitModel.Description != null)
                        {
                            ProductUpdateLogData.AppendLine($"{ProductToUpdate.Description} => {SubmitModel.Description}");
                            ProductToUpdate.Description = SubmitModel.Description;
                        }
                        if (SubmitModel.Price != null)
                        {
                            ProductUpdateLogData.AppendLine($"{ProductToUpdate.Price} => {SubmitModel.Price}");
                            ProductToUpdate.Price = SubmitModel.Price;
                        }
                        if (SubmitModel.ExpireDate != null)
                        {
                            ProductUpdateLogData.AppendLine($"{ProductToUpdate.ExpireDate} => {SubmitModel.ExpireDate}");
                            ProductToUpdate.ExpireDate = SubmitModel.ExpireDate;
                        }
                        if (SubmitModel.DateAdded != null)
                        {
                            ProductUpdateLogData.AppendLine($"{ProductToUpdate.DateAdded} => {SubmitModel.DateAdded}");
                            ProductToUpdate.DateAdded = SubmitModel.DateAdded;
                        }

                        if (ProductUpdateLogData.Length > 0)
                        {
                            int? UserID = null;
                            if (!string.IsNullOrWhiteSpace(UserEmail))
                            {
                                //var User = 
                            }
                            db.UserActionLogs.Add(new UserActionLog
                            {
                                LogActionUrl = nameof(UpdateProduct),
                                LogActionDescription = ProductUpdateLogData.ToString(),
                                LogDateCreated = DateTime.Now
                            });
                        }

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
                using (var db = new SimpleShopDBModel())
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
            public string Description { get; set; }
            public decimal? Price { get; set; }
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