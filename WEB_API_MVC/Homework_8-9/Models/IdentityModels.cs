using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework_8_9.Models
{
    public class TokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string userName { get; set; }
    }
}