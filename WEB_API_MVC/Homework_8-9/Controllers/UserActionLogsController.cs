using Homework_8_9.Helpers;
using Homework_8_9.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Homework_8_9.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    public class UserActionLogsController : WebsiteControllerBase
    {
        public async Task<ActionResult> Index()
        {
            var ViewModel = await UserActionLogsModel.GetPageViewModel();
            return View(ViewModel);
        }
    }
}