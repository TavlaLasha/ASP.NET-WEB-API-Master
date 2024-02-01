using BLL.Contracts;
using Models.DataViewModels.CurrencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CurrencyService.Controllers
{
    public class LogController : ApiController
    {
        readonly ILogManagement logyManagement;
        public LogController(ILogManagement logyManagement)
        {
            this.logyManagement = logyManagement;
        }

        [Route("api/Log/GetAll")]
        [HttpGet]
        public IEnumerable<LogDTO> GetAllCurrencies() => logyManagement.GetAllLogs();
    }
}
