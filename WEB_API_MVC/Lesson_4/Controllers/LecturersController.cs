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
    public class LecturersController : ApiControllerBase<LecturersModel>
    {
        #region Constructors
        public LecturersController()
        {
            Model = new LecturersModel();
        }
        #endregion

        [HttpGet]
        public HttpResponseMessage Index()
        {
            var Result = Model.GetLecturers();
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPost]
        public HttpResponseMessage AddLecturer([FromBody] LecturersModel.LecturerDTO SubmitModel)
        {
            var Result = Model.AddLecturer(SubmitModel);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPut]
        public HttpResponseMessage UpdateLecturer([FromBody] LecturersModel.LecturerDTO SubmitModel)
        {
            var Result = Model.UpdateLecturer(SubmitModel);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteLecturer(int? id)
        {
            var Result = Model.DeleteLecturer(id);
            return Request.CreateResponse(Result.StatusCode, Result);
        }
    }
}