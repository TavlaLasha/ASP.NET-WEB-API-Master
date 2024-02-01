using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CurrencyApp.Helpers
{
    public interface ISessionAssistance
    {
        #region Methods
        void Clear();
        T Get<T>(string Key);
        bool HasKey(string Key);
        void Set<T>(string Key, T Value);
        void Remove(string Key);
        #endregion
    }
}