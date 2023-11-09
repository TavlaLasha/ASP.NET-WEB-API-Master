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
    public class SubjectsController : ApiControllerBase<SubjectsModel>
    {
        #region Constructors
        public SubjectsController()
        {
            Model = new SubjectsModel();
        }
        #endregion

        [HttpGet]
        public HttpResponseMessage Index()
        {
            var Result = Model.GetSubjects();
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPost]
        public HttpResponseMessage AddSubject([FromBody] SubjectsModel.SubjectDTO SubmitModel)
        {
            var Result = Model.AddSubject(SubmitModel);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPut]
        public HttpResponseMessage UpdateSubject([FromBody] SubjectsModel.SubjectDTO SubmitModel)
        {
            var Result = Model.UpdateSubject(SubmitModel);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteSubject(int? id)
        {
            var Result = Model.DeleteSubject(id);
            return Request.CreateResponse(Result.StatusCode, Result);
        }
    }
}