using Homework_3_4.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Web.Http;
using System.Web.Http.Results;

namespace Homework_3_4.Controllers
{
    [RoutePrefix("test_one")]
    public class Test1Controller : ApiController
    {
        [HttpGet]
        [Route("", Name = "GetAll")]
        public HttpResponseMessage Index()
        {
            var Result = new TestDTO
            {
                Fullname = "Lasha Tavlalashvili",
                PersonalNumber = "123456678781"
            };
            var stream1 = new MemoryStream();
            var ser = new DataContractJsonSerializer(typeof(TestDTO));
            ser.WriteObject(stream1, Result);
            stream1.Position = 0;
            var sr = new StreamReader(stream1);
            return Request.CreateResponse(HttpStatusCode.OK, sr.ReadToEnd());
        }

        [HttpPost]
        [Route("add", Name = "Add")]
        public HttpResponseMessage AddCurriculum([FromBody] string SubmitModel)
        {
            return Request.CreateResponse(HttpStatusCode.Created, SubmitModel);
        }

        [HttpPut]
        [Route("update", Name = "Update")]
        public HttpResponseMessage UpdateCurriculum([FromBody] string SubmitModel)
        {
            return Request.CreateResponse(HttpStatusCode.OK, SubmitModel);
        }

        [HttpDelete]
        [Route("delete/{id:int}", Name = "Delete")]
        public HttpResponseMessage DeleteCurriculum(int? id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, id);
        }

        [HttpGet]
        [Route("test")]
        public IHttpActionResult Test()
        {
            return Ok();
        }

        [HttpGet]
        [Route("interesting-test")]
        public HttpResponseMessage Test2()
        {
            return Request.CreateResponse(418);
        }
    }
}
