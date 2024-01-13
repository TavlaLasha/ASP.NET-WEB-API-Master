using Homework_8_9.Filters;
using Homework_8_9.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework_8_9.Controllers
{
    [BeforeWebsitePageLoad(Order = -1)]
    public class WebsiteControllerBase : Controller
    {
        public WebsiteLayoutViewModel LayoutViewModel { get; set; }
    }
}