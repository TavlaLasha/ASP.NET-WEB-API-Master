using Newtonsoft.Json;
using System.Linq;
using System.Web;

namespace Homework_8_9.Helpers
{

    public class SessionAssistance : ISessionAssistance
    {
        #region Properties
        HttpSessionStateBase Session;
        #endregion

        #region Constructors
        public SessionAssistance(HttpSessionStateBase Session)
        {
            this.Session = Session;
        }
        #endregion

        #region Methods
        public void Clear()
        {
            Session.Clear();
        }

        public T Get<T>(string Key)
        {
            return HasKey(Key) ? JsonConvert.DeserializeObject<T>(Session[Key].ToString()) : default(T);
        }

        public bool HasKey(string Key)
        {
            return Session[Key] != null;
        }

        public void Set<T>(string Key, T Value)
        {
            Session[Key] = JsonConvert.SerializeObject(Value);
        }

        public void Remove(string Key)
        {
            Session.Remove(Key);
        }
        #endregion
    }
}
