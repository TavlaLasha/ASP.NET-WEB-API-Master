using Homework_8_9_API.Models;
using System.Net.Http;
using System.Web.Http;

namespace Homework_8_9_API.Controllers
{
    [RoutePrefix("api/logs")]
    public class UserActionLogsController : ApiControllerBase<UserActionLogsModel>
    {
        #region Constructors
        public UserActionLogsController()
        {
            Model = new UserActionLogsModel();
        }
        #endregion

        [HttpGet]
        [Route("", Name = "GetAllUserActionLogs")]
        public HttpResponseMessage Index(int? id = null)
        {
            var Result = Model.GetUserActionLogs(id);
            return Request.CreateResponse(Result.StatusCode, Result);
        }
    }
}