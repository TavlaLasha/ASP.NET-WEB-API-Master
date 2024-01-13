using Homework_8_9_API.EF;
using Homework_8_9_API.Reusables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace Homework_8_9_API.Models
{
    public class UserActionLogsModel
    {
        #region Methods
        public UserActionLogsResponse GetUserActionLogs(int? ParentID)
        {
            var Result = new UserActionLogsResponse();

            using (var db = new SimpleShopDBModel())
            {
                Result.UserActionLogs = db.UserActionLogs.Select(i => new UserActionLogDTO
                {
                    LogActionUserEmail = i.AspNetUser.Email,
                    LogActionUrl = i.LogActionUrl,
                    LogActionDescription = i.LogActionDescription,
                    LogDateCreated = i.LogDateCreated

                }).ToList();
                Result.IsSuccess = true;
            }

            return Result;
        }
        #endregion

        #region Sub Classes
        public class UserActionLogDTO
        {
            public string LogActionUserEmail { get; set; }
            public string LogActionUrl { get; set; }
            public string LogActionDescription { get; set; }
            public DateTime LogDateCreated { get; set; }
        }

        public class UserActionLogsResponse : ApiResponseBase
        {
            public List<UserActionLogDTO> UserActionLogs { get; set; }
        }
        #endregion
    }
}