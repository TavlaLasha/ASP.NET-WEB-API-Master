using Homework_8_9.DTOModels;
using Homework_8_9.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Homework_8_9.Models
{
    public class UserActionLogsModel
    {
        static string BaseURL = ConfigurationManager.AppSettings["ShopService"];

        public static async Task<PageViewModel> GetPageViewModel()
        {
            var ViewModel = new PageViewModel();

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync($"{BaseURL}api/logs");

                var Content = await response.Content.ReadAsStringAsync();
                var Result = JsonConvert.DeserializeObject<UserActionLogsResponse>(Content);

                ViewModel.UserActionLogs = Result.UserActionLogs?.Select(Item => new PageViewModel.UserActionLogViewModel
                {
                    LogActionUserEmail = Item.LogActionUserEmail,
                    LogActionUrl = Item.LogActionUrl,
                    LogActionDescription = Item.LogActionDescription,
                    LogDateCreated = Item.LogDateCreated.ToString("dd-MM-yyy HH:mm"),
                }).ToList();
            }

            return ViewModel;
        }

        public class PageViewModel
        {
            public List<UserActionLogViewModel> UserActionLogs { get; set; }
            public bool HasUserActionLogs => UserActionLogs?.Count > 0;

            public class UserActionLogViewModel
            {
                public string LogActionUserEmail { get; set; }
                public string LogActionUrl { get; set; }
                public string LogActionDescription { get; set; }
                public string LogDateCreated { get; set; }
            }
        }

        public class UserActionLogsResponse : ApiResponseBase
        {
            public List<UserActionLogDTO> UserActionLogs { get; set; }
        }
    }
}