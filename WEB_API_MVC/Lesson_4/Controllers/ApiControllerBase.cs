using System.Web.Http;

namespace Lesson_4.Controllers
{
    public class ApiControllerBase<T> : ApiController
    {
        public T Model { get; set; }
    }
}