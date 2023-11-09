using Lesson_4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Lesson_4.Controllers
{
    [RoutePrefix("curriculums")]
    public class CurriculumsController : ApiControllerBase<CurriculumsModel>
    {
        #region Constructors
        public CurriculumsController()
        {
            Model = new CurriculumsModel();
        }
        #endregion

        [HttpGet]
        [Route("", Name = "GetAllCuriculums")]
        public HttpResponseMessage Index()
        {
            var Result = Model.GetCurriculums();
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPost]
        [Route("add", Name = "AddCuriculum")]
        public HttpResponseMessage AddCurriculum([FromBody] CurriculumsModel.CurriculumDTO SubmitModel)
        {
            var Result = Model.AddCurriculum(SubmitModel);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPut]
        [Route("update", Name = "UpdateCuriculum")]
        public HttpResponseMessage UpdateCurriculum([FromBody] CurriculumsModel.CurriculumDTO SubmitModel)
        {
            var Result = Model.UpdateCurriculum(SubmitModel);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpDelete]
        [Route("delete/{id:int}", Name = "DeleteCuriculum")]
        public HttpResponseMessage DeleteCurriculum(int? id)
        {
            var Result = Model.DeleteCurriculum(id);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpGet]
        [Route("test")]
        public IHttpActionResult Test()
        {
            return Ok();
            //return BadRequest();
            //return NotFound();
        }
        
        [HttpGet]
        [Route("test-for-return-type")]
        public bool Test2()
        {
            return true;
            //return BadRequest();
            //return NotFound();
        }
    }
}