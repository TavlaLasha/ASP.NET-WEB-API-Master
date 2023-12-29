using Homework_8_9_API.EF;
using Homework_8_9_API.Reusables;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Homework_8_9_API.Models
{
    public class ProductCategoriesModel
    {
        #region Methods
        public ProductCategoriesResponse GetProductCategories(int? ParentID)
        {
            var Result = new ProductCategoriesResponse();

            using (var db = new SimpleShopDBModel())
            {
                Result.ProductCategories = db.ProductCategories.Select(i => new ProductCategoryDTO
                {
                    ProductCategoryID = i.ProductCategoryID,
                    ProductCategoryParentID = i.ProductCategoryParentID,
                    ProductCategoryName = i.ProductCategoryName

                }).Where(Item => ParentID == null || Item.ProductCategoryParentID == ParentID).ToList();
                Result.IsSuccess = true;
            }

            return Result;
        }

        public ProductResponse GetSingleProductCategory(int? ProductCategoryID)
        {
            var Result = new ProductResponse();

            using (var db = new SimpleShopDBModel())
            {
                var DBItem = db.ProductCategories.FirstOrDefault(i => i.ProductCategoryID == ProductCategoryID);
                if (DBItem != null)
                {
                    Result.Product = new ProductCategoryDTO
                    {
                        ProductCategoryID = DBItem.ProductCategoryID,
                        ProductCategoryParentID = DBItem.ProductCategoryParentID,
                        ProductCategoryName = DBItem.ProductCategoryName

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

        public ApiResponseBase AddProductCategory(ProductCategoryDTO SubmitModel)
        {
            var Result = new ApiResponseBase();
            if (SubmitModel != null)
            {
                using (var db = new SimpleShopDBModel())
                {
                    ProductCategory Product = new ProductCategory
                    {
                        ProductCategoryParentID = SubmitModel.ProductCategoryParentID,
                        ProductCategoryName = SubmitModel.ProductCategoryName
                    };
                    db.ProductCategories.Add(Product);
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

        public ApiResponseBase UpdateProductCategory(ProductCategoryDTO SubmitModel)
        {
            var Result = new ApiResponseBase();

            if (SubmitModel != null)
            {
                using (var db = new SimpleShopDBModel())
                {
                    if (SubmitModel.ProductCategoryID != null && !db.ProductCategories.Any(i => i.ProductCategoryID == SubmitModel.ProductCategoryID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var ProductToUpdate = db.ProductCategories.Where(i => i.ProductCategoryID == SubmitModel.ProductCategoryID).First();

                        ProductToUpdate.ProductCategoryParentID = SubmitModel.ProductCategoryParentID;
                        ProductToUpdate.ProductCategoryName = SubmitModel.ProductCategoryName;
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }

        public ApiResponseBase DeleteProductCategory(int? ProductCategoryID)
        {
            var Result = new ApiResponseBase();

            if (ProductCategoryID != null)
            {
                using (var db = new SimpleShopDBModel())
                {
                    if (!db.ProductCategories.Any(i => i.ProductCategoryID == ProductCategoryID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var r = db.ProductCategories.Where(i => i.ProductCategoryID == ProductCategoryID).First();
                        db.ProductCategories.Remove(r);
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }
        #endregion

        #region Sub Classes
        public class ProductCategoryDTO
        {
            public int? ProductCategoryID { get; set; }
            public int? ProductCategoryParentID { get; set; }
            public string ProductCategoryName { get; set; }
        }

        public class ProductCategoriesResponse : ApiResponseBase
        {
            public List<ProductCategoryDTO> ProductCategories { get; set; }
        }

        public class ProductResponse : ApiResponseBase
        {
            public ProductCategoryDTO Product { get; set; }
        }
        #endregion
    }
}