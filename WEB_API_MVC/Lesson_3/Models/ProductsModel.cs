using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson_3.Models
{
    public class ProductsModel
    {
        static List<ProductModel> Products = new List<ProductModel>
        {
            new ProductModel
            {
                Id = 1,
                Name = "Test",
                Price = 2.20m
            },
            new ProductModel
            {
                Id = 2,
                Name = "Coca-Cola",
                Price = 3.45m
            },
            new ProductModel
            {
                Id = 3,
                Name = "Fanta",
                Price = 3.20m
            }
        };

        #region Methods
        public List<ProductModel> GetProducts()
        {            
            return Products;
        }

        public bool AddProduct(ProductModel SubmitModel)
        {
            bool Result = false;
            if(SubmitModel != null) 
            {
                Products.Add(SubmitModel);
                Result = true;
            }
            return Result;
        }

        public bool UpdateProduct(ProductModel SubmitModel)
        {
            bool Result = false;

            if (SubmitModel != null)
            {
                var ProductToUpdate = Products.FirstOrDefault(i => i.Id == SubmitModel.Id);
                if(ProductToUpdate != null)
                {
                    ProductToUpdate.Name = SubmitModel.Name;
                    ProductToUpdate.Price = SubmitModel.Price;
                    Result = true;
                }
            }

            return Result;
        }

        public bool DeleteProduct(int? ProductID)
        {
            bool Result = false;

            if (ProductID != null)
            {
                var ProductToRemove = Products.FirstOrDefault(i => i.Id == ProductID);
                if (ProductToRemove != null)
                {
                    Products.Remove(ProductToRemove);
                    Result = true;
                }
            }

            return Result;
        }
        #endregion

        #region Sub Classes
        public class ProductModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal? Price { get; set; }
        }
        #endregion
    }
}