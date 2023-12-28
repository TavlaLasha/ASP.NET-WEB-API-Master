using System;
using System.Web;

namespace Homework_8_9.Helpers
{
    public class CookieAssistance : ICookieAssistance
    {
        #region Properties
        HttpRequest Request;
        HttpResponse Response;
        #endregion

        #region Constructors
        public CookieAssistance(HttpRequest Request, HttpResponse Response)
        {
            this.Request = Request;
            this.Response = Response;
        }

        public T Get<T>(string Key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string Key)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string Key, T Value, DateTime? ExpirationDate)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods        
        //public T Get<T>(string Key)
        //{
        //    if (Request.Cookies.GetKey(Key))
        //    {
        //        return Request.Cookies[Key].DeserializeJsonTo<T>();
        //    }
        //    else
        //    {
        //        return default(T);
        //    }
        //}

        //public void Set<T>(string Key, T Value, DateTime? ExpirationDate = null)
        //{
        //    if (!string.IsNullOrWhiteSpace(Key))
        //    {
        //        var options = new CookieOptions();
        //        options.Expires = ExpirationDate.HasValue ? ExpirationDate.Value : DateTime.Now.AddDays(1);
        //        Response.Cookies.Append(Key, Value.ToJson());
        //    }
        //}

        //public void Remove(string Key)
        //{
        //    if (Request.Cookies.ContainsKey(Key))
        //    {
        //        Response.Cookies.Delete(Key);
        //    }
        //}
        #endregion
    }
}
