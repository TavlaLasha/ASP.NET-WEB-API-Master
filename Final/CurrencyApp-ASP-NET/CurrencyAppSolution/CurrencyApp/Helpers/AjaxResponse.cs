using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyApp.Helpers
{
    public class AjaxResponse
    {
        public bool IsSuccess { get; set; }

        public dynamic Data { get; set; }
    }
}