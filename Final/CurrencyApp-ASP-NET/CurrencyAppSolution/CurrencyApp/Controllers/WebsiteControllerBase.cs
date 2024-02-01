using CurrencyApp.Filters;
using CurrencyApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CurrencyApp.Controllers
{
    [BeforeWebsitePageLoad(Order = -1)]
    public class WebsiteControllerBase : Controller
    {
        public WebsiteLayoutViewModel LayoutViewModel { get; set; }
    }
}