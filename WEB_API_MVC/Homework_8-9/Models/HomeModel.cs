using Homework_8_9.DTOModels;
using Homework_8_9.Helpers;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Homework_8_9.Models
{
    public class HomeModel
    {
        static string BaseURL = ConfigurationManager.AppSettings["ShopService"];

        public static async Task<PageViewModel> GetPageViewModel()
        {
            var ViewModel = new PageViewModel();
            ViewModel.UrlGetProductCategories = $"{BaseURL}api/product-categories";
            ViewModel.UrlGetProducts = $"{BaseURL}api/products";
            ViewModel.UrlUpdateProductPrice = "/Home/UpdateProductPrice";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(ViewModel.UrlGetProductCategories);

                var Content = await response.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<ProductCategoriesResponse>(Content);

                ViewModel.ParentProductCategories = Result.ProductCategories?.Where(i => i.ProductCategoryParentID == null).ToList();
            }

            return ViewModel;
        }

        public static async Task<AjaxResponse> UpdateProductPrice(int? productID, decimal? newPrice)
        {
            var AR = new AjaxResponse();

            var Data = new ProductDTO
            {
                ID = productID,
                Price = newPrice
            };

            string output = JsonConvert.SerializeObject(Data);
            var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var User = SessionAssistance.GetUser(new HttpSessionStateWrapper(HttpContext.Current.Session));
                if (User != null)
                {
                    client.DefaultRequestHeaders.Add("UserEmail", User.Email);
                }

                var response = await client.PutAsync($"{BaseURL}api/products/update", stringContent);

                var Content = await response.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<ApiResponseBase>(Content);
                AR.IsSuccess = Result.IsSuccess;
            }

            return AR;
        }

        public class PageViewModel
        {
            public List<ProductCategoryDTO> ParentProductCategories { get; set; }
            public bool HasProductCategories => ParentProductCategories?.Count > 0;
            public string UrlGetProductCategories { get; set; }
            public string UrlGetProducts { get; set; }
            public string UrlUpdateProductPrice { get; set; }
        }

        public class ProductCategoriesResponse : ApiResponseBase
        {
            public List<ProductCategoryDTO> ProductCategories { get; set; }
        }
    }
}