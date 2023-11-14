using Midterm_WEB_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Midterm_WEB_API.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiControllerBase<CustomersModel>
    {
        #region Constructors
        public CustomersController()
        {
            Model = new CustomersModel();
        }
        #endregion

        [HttpGet]
        [Route("", Name = "GetAllCustomers")]
        public HttpResponseMessage Index()
        {
            var Result = Model.GetCustomers();
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetSingleCustomer")]
        public HttpResponseMessage GetSingle(int? id)
        {
            var Result = Model.GetSingleCustomer(id);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPost]
        [Route("add", Name = "AddCustomer")]
        public HttpResponseMessage AddCustomer([FromBody] CustomersModel.CustomerDTO SubmitModel)
        {
            var Result = Model.AddCustomer(SubmitModel);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPut]
        [Route("update", Name = "UpdateCustomer")]
        public HttpResponseMessage UpdateCustomer([FromBody] CustomersModel.CustomerDTO SubmitModel)
        {
            var Result = Model.UpdateCustomer(SubmitModel);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpDelete]
        [Route("delete/{id:int}", Name = "DeleteCustomer")]
        public HttpResponseMessage DeleteCustomer(int? id)
        {
            var Result = Model.DeleteCustomer(id);
            return Request.CreateResponse(Result.StatusCode, Result);
        }
    }
}
