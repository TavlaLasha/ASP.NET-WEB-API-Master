using Homework_8_9.DTOModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Homework_8_9.Models
{
    public class HomeModel
    {
        static HttpClient client = new HttpClient();
        static string BaseURL = ConfigurationManager.AppSettings["ShopService"];

        public async Task<PageViewModel> GetPageViewModel()
        {
            var ViewModel = new PageViewModel();
            ViewModel.UrlGetProductCategories = $"{BaseURL}api/product-categories";
            ViewModel.UrlGetProducts = $"{BaseURL}api/products";

            var response = await client.GetAsync(ViewModel.UrlGetProductCategories);

            var Content = await response.Content.ReadAsStringAsync();
            var Result = JsonConvert.DeserializeObject<ProductCategoriesResponse>(Content);

            ViewModel.ParentProductCategories = Result.ProductCategories?.Where(i => i.ProductCategoryParentID == null).ToList();

            return ViewModel;
        }

        public class PageViewModel
        {
            public List<ProductCategoryDTO> ParentProductCategories { get; set; }
            public bool HasProductCategories => ParentProductCategories?.Count > 0;
            public string UrlGetProductCategories { get; set; }
            public string UrlGetProducts { get; set; }
        }

        public class ProductCategoriesResponse : ApiResponseBase
        {
            public List<ProductCategoryDTO> ProductCategories { get; set; }
        }
    }
}