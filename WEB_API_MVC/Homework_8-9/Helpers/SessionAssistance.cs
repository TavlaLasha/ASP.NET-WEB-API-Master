using Homework_8_9.Models;
using Newtonsoft.Json;
using System.Linq;
using System.Web;

namespace Homework_8_9.Helpers
{

    public class SessionAssistance
    {
        #region Methods
        public static void Clear(HttpSessionStateBase Session)
        {
            Session.Clear();
        }

        public static void SetValue(HttpSessionStateBase Session, string Key, object Value)
        {
            Session[Key] = Value;
        }

        public static T GetValue<T>(HttpSessionStateBase Session, string Key)
        {
            return (T)Session[Key];
        }

        public static void SetUser(HttpSessionStateBase Session, object User)
        {
            SetValue(Session, "user", User);
        }

        public static User GetUser(HttpSessionStateBase Session)
        {
            return Session["user"] as User;
        }

        public static void Remove(HttpSessionStateBase Session, string Key)
        {
            Session.Remove(Key);
        }
        #endregion
    }
}
