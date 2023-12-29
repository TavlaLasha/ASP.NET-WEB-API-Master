using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Homework_8_9.Models
{
    public class ApiResponseBase
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}