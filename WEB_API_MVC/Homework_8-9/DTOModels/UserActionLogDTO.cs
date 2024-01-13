using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework_8_9.DTOModels
{
    public class UserActionLogDTO
    {
        public string LogActionUserEmail { get; set; }
        public string LogActionUrl { get; set; }
        public string LogActionDescription { get; set; }
        public DateTime LogDateCreated { get; set; }
    }
}