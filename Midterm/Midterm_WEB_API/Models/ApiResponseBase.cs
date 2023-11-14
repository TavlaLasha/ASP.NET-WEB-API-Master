using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Midterm_WEB_API.Models
{
    public class ApiResponseBase
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}