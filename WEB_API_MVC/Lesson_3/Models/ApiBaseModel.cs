using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lesson_3.Models
{
    public class ApiBaseModel
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}