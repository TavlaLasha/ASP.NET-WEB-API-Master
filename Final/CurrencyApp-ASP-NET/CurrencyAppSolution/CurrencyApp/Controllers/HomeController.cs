using CurrencyApp.Helpers;
using CurrencyApp.Models;
using Models.DataViewModels.CurrencyManagement;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        [CustomAuthorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> FillDBWithNew()
        {
            var AR = await HomeModel.FillDBWithNew();
            if (AR.IsSuccess)
            {
                TempData["codes"] = AR.Data;
                return RedirectToAction("Index");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Edit(string code)
        {
            var ViewModel = await HomeModel.GetEditPartialViewModel(code);
            return PartialView("_EditPartial", ViewModel);
        }

        [CustomAuthorize(Roles = "Admin")]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> Edit(string code, [Bind(Include = "quantity, rateFormated, diffFormated, rate, name, diff, date, validFromDate")] CurrencyDTO currency)
        {
            bool? Result = null;

            if (!ModelState.IsValid)
            {
                Result = false;
            }
            else
            {
                Result = await HomeModel.EditCurrency(code, currency);
            }

            if (Result == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (Result == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
            else
            {
                return Json(new { Changed = Result == true, JsonRequestBehavior.AllowGet });
            }
        }

        [CustomAuthorize(Roles = "Admin,Manager")]
        public async Task<ActionResult> GetLogs(int page = 1)
        {
            var ViewModel = await HomeModel.GetLogsPageViewModel(page);
            return View(ViewModel);
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