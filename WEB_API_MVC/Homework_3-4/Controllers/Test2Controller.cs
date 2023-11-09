using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Homework_3_4.Controllers
{
    public class Test2Controller : ApiController
    {
        public IHttpActionResult GetAll()
        {
            return Json(new List<int> { 1,2,3,4});
        }

        public string GetSingle(int id)
        {
            return "test1 value";
        }

        public void Add([FromBody] string value)
        {
        }

        public void Update(int id, [FromBody] string value)
        {
        }

        public void Delete(int id)
        {
        }
    }
}
