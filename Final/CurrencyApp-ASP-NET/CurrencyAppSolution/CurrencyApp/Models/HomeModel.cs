using CurrencyApp.Helpers;
using Models.DataViewModels.CurrencyManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace CurrencyApp.Models
{
    public class HomeModel
    {
        static string BaseURL = ConfigurationManager.AppSettings["CurrencyService"] + "api/";

        public static async Task<PageViewModel> GetPageViewModel()
        {
            var ViewModel = new PageViewModel();
            var User = SessionAssistance.GetUser(new HttpSessionStateWrapper(HttpContext.Current.Session));
            ViewModel.UserIsAdmin = User?.Roles?.Any(Item => Item == "Admin") == true;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseURL}Currency/GetAll");
                if (response.IsSuccessStatusCode)
                {
                    ViewModel.Currencies = JsonConvert.DeserializeObject<List<CurrencyDTO>>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return ViewModel;
        }

        //public static async Task<AjaxResponse> UpdateProductPrice(int? productID, decimal? newPrice)
        //{
        //    var AR = new AjaxResponse();

        //    var Data = new ProductDTO
        //    {
        //        ID = productID,
        //        Price = newPrice
        //    };

        //    string output = JsonConvert.SerializeObject(Data);
        //    var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

        //    using (var client = new HttpClient())
        //    {
        //        var User = SessionAssistance.GetUser(new HttpSessionStateWrapper(HttpContext.Current.Session));
        //        if (User != null)
        //        {
        //            client.DefaultRequestHeaders.Add("UserEmail", User.Email);
        //        }

        //        var response = await client.PutAsync($"{BaseURL}api/products/update", stringContent);

        //        var Content = await response.Content.ReadAsStringAsync();
        //        var Result = JsonConvert.DeserializeObject<ApiResponseBase>(Content);
        //        AR.IsSuccess = Result.IsSuccess;
        //    }

        //    return AR;
        //}

        public class PageViewModel
        {
            public List<CurrencyDTO> Currencies { get; set; }
            public bool HasCurrencies => Currencies?.Count > 0;
            public bool UserIsAdmin {  get; set; }
        }
    }
}