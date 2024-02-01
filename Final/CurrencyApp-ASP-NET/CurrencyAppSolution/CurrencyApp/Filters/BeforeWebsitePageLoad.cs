using CurrencyApp.Controllers;
using CurrencyApp.Helpers;
using CurrencyApp.Models;
using System.Web.Mvc;

namespace CurrencyApp.Filters
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