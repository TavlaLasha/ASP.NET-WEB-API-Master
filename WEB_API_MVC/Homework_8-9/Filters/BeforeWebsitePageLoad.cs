using Homework_8_9.Controllers;
using Homework_8_9.Helpers;
using Homework_8_9.Models;
using System.Web.Mvc;

namespace Homework_8_9.Filters
{
    public class BeforeWebsitePageLoad : FilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext FilterContext)
        {

        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext FilterContext)
        {
            var C = FilterContext.Controller as WebsiteControllerBase;
            var Model = C.LayoutViewModel = new WebsiteLayoutViewModel();

            Model.User = SessionAssistance.GetUser(C.Session);

            C.ViewData["ConstantsViewDataLayoutViewModel"] = Model;
        }

    }
}