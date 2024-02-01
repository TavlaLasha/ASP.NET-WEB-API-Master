using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyApp.Helpers
{
    public interface ICookieAssistance
    {
        #region Methods
        T Get<T>(string Key);
        void Set<T>(string Key, T Value, DateTime? ExpirationDate);
        void Remove(string Key);
        #endregion
    }
}