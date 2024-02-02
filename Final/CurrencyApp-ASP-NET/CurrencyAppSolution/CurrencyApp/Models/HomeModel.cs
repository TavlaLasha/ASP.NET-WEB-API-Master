using CurrencyApp.Helpers;
using Models.DataViewModels.CurrencyManagement;
using Newtonsoft.Json;
using PagedList;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CurrencyApp.Models
{
    public class HomeModel
    {
        static string BaseURL = ConfigurationManager.AppSettings["CurrencyService"] + "api/";

        public static async Task<PageViewModel> GetPageViewModel()
        {
            var ViewModel = new PageViewModel();
            var User = SessionAssistance.GetUser(new HttpSessionStateWrapper(HttpContext.Current.Session));
            ViewModel.UserIsAdmin = User?.IsInRole("Admin") == true;
            ViewModel.UserIsManager = User?.IsInRole("Manager") == true;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{BaseURL}Currency/GetAll");
                if (response.IsSuccessStatusCode)
                {
                    ViewModel.Currencies = JsonConvert.DeserializeObject<List<CurrencyDTO>>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return ViewModel;
        }

        public static async Task<AjaxResponse> FillDBWithNew()
        {
            var AR = new AjaxResponse();

            var User = SessionAssistance.GetUser(new HttpSessionStateWrapper(HttpContext.Current.Session));
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{BaseURL}Currency/FillDBWithLatest/{User.Email.Substring(0, User.Email.IndexOf("@"))}");
                if (response.IsSuccessStatusCode)
                {
                    AR.Data = JsonConvert.DeserializeObject<List<string>>(response.Content.ReadAsStringAsync().Result); ;
                    AR.IsSuccess = true;
                }
            }

            return AR;
        }

        public static async Task<CurrencyDTO> GetEditPartialViewModel(string code)
        {
            var ViewModel = new CurrencyDTO();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{BaseURL}Currency/GetCurrency/{code}");
                if (response.IsSuccessStatusCode)
                {
                    ViewModel = JsonConvert.DeserializeObject<CurrencyDTO>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return ViewModel;
        }

        public static async Task<bool?> EditCurrency(string code, CurrencyDTO currency)
        {
            bool? Result = null;

            string output = JsonConvert.SerializeObject(currency);
            var stringContent = new StringContent(output, Encoding.UTF8, "application/json");
            var User = SessionAssistance.GetUser(new HttpSessionStateWrapper(HttpContext.Current.Session));
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync($"{BaseURL}Currency/EditCurrency/{code}/{User.Email.Substring(0, User.Email.IndexOf("@"))}", stringContent);

                if (response.IsSuccessStatusCode)
                {
                    Result = JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
                }
            }

            return Result;
        }

        public static async Task<PagedList<LogDTO>> GetLogsPageViewModel(int page)
        {
            PagedList<LogDTO> ViewModel = null;

            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync($"{BaseURL}Log/GetAll");
                List<LogDTO> ct = new List<LogDTO>();
                if (response.IsSuccessStatusCode)
                {
                    ct = JsonConvert.DeserializeObject<List<LogDTO>>(response.Content.ReadAsStringAsync().Result);
                }
                ViewModel = new PagedList<LogDTO>(ct, page, 15);
            }

            return ViewModel;
        }

        public class PageViewModel
        {
            public List<CurrencyDTO> Currencies { get; set; }
            public bool HasCurrencies => Currencies?.Count > 0;
            public bool UserIsAdmin { get; set; }
            public bool UserIsManager { get; set; }
        }
    }
}