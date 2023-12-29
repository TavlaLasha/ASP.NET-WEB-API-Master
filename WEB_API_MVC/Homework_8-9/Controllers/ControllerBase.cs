using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Homework_8_9.Controllers
{
    public class ControllerBase<T> : Controller
    {
        public T Model { get; set; }
    }
}