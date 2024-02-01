﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyApp.Models
{
    public class WebsiteLayoutViewModel
    {
        #region Properties
        public User User { get; set; }
        public bool IsAuthenticated => User != null;
        public bool IsAdmin => User?.Roles?.Any(Item => Item.Equals("Admin")) == true;
        #endregion
    }
}