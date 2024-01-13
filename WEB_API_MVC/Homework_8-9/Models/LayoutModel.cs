using System.Collections.Generic;
using System.Linq;

namespace Homework_8_9.Models
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
