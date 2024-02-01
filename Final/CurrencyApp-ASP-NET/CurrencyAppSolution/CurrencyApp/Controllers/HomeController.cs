using Models.DataViewModels.CurrencyManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PagedList;
using CurrencyApp.Helpers;
using System.Threading.Tasks;
using CurrencyApp.Models;

namespace CurrencyApp.Controllers
{
    public class HomeController : WebsiteControllerBase
    {
        static HttpClient client = new HttpClient();
        string BaseURL = ConfigurationManager.AppSettings["CurrencyService"] + "api/";
        public async Task<ActionResult> Index()
        {
            var ViewModel = await HomeModel.GetPageViewModel();

            return View(ViewModel);
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult FillDBWithNew()
        {
            try
            {
                var User = SessionAssistance.GetUser(Session);
                HttpResponseMessage response = client.GetAsync($"{BaseURL}Currency/FillDBWithLatest/{User.Email}").Result;
                List<string> updated = new List<string>();
                if (response.IsSuccessStatusCode)
                {
                    updated = JsonConvert.DeserializeObject<List<string>>(response.Content.ReadAsStringAsync().Result);
                }
                TempData["codes"] = updated;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Edit(string code)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync($"{BaseURL}Currency/GetCurrency/{code}").Result;
                CurrencyDTO ct = new CurrencyDTO();
                if (response.IsSuccessStatusCode)
                {
                    ct = JsonConvert.DeserializeObject<CurrencyDTO>(response.Content.ReadAsStringAsync().Result);
                }
                return PartialView("_EditPartial", ct);
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Edit(string code, [Bind(Include = "quantity, rateFormated, diffFormated, rate, name, diff, date, validFromDate")] CurrencyDTO currency)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("მონაცემები არავალიდურია!");

                string output = JsonConvert.SerializeObject(currency);
                var stringContent = new StringContent(output, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsync($"{BaseURL}Currency/EditCurrency/{code}/{User.Identity.Name.Substring(0, User.Identity.Name.IndexOf("@"))}", stringContent).Result;

                bool updated;
                if (response.IsSuccessStatusCode)
                {
                    updated = JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
                bool changed = false;
                if (updated)
                    changed = true;


                return Json(new { Changed = changed, JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }

        public ActionResult GetLogs(int page=1)
        {
            try
            {
                HttpResponseMessage response = client.GetAsync($"{BaseURL}Log/GetAll").Result;
                List<LogDTO> ct = new List<LogDTO>();
                if (response.IsSuccessStatusCode)
                {
                    ct = JsonConvert.DeserializeObject<List<LogDTO>>(response.Content.ReadAsStringAsync().Result);
                }
                PagedList<LogDTO> model = new PagedList<LogDTO>(ct, page, 15);

                return View(model);
            }
            catch (Exception ex)
            {
                return new HttpNotFoundResult();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}