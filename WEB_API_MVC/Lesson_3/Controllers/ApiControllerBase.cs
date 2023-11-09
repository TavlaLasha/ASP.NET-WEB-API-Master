using System.Web.Http;

namespace Lesson_3.Controllers
{
    public class ApiControllerBase<T> : ApiController
    {
        public T Model { get; set; }
    }
}