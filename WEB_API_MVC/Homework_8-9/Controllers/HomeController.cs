using Homework_8_9.Helpers;
using Homework_8_9.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Homework_8_9.Controllers
{
    [CustomAuthorize]
    public class HomeController : WebsiteControllerBase
    {
        //[AllowAnonymous]
        public async Task<ActionResult> Index()
        {
            var ViewModel = await HomeModel.GetPageViewModel();

            ViewBag.MyCustomText = "This message has been delivered with the help of ViewBag";

            return View(ViewModel);
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateProductPrice(int? productID, decimal? newPrice)
        {
            var AR = await HomeModel.UpdateProductPrice(productID, newPrice);

            return Json(AR);
        }

        [CustomAuthorize(Roles = "Admin")]
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