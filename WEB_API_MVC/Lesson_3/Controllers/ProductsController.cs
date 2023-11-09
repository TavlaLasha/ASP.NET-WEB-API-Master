using Lesson_3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Lesson_3.Controllers
{
    public class ProductsController : ApiControllerBase<ProductsModel>
    {
        #region Constructors
        public ProductsController()
        {
            Model = new ProductsModel();
        }
        #endregion

        [HttpGet]
        public HttpResponseMessage Index()
        {
            var Result = Model.GetProducts();
            return Request.CreateResponse(HttpStatusCode.OK, Result);
        }

        [HttpPost]
        public HttpResponseMessage AddProduct([FromBody] ProductsModel.ProductModel SubmitModel)
        {
            var Result = Model.AddProduct(SubmitModel);
            return Request.CreateResponse(Result ?  HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }

        [HttpPut]
        public HttpResponseMessage UpdateProduct([FromBody] ProductsModel.ProductModel SubmitModel)
        {
            var Result = Model.UpdateProduct(SubmitModel);
            return Request.CreateResponse(Result ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }

        [HttpDelete]
        public HttpResponseMessage DeleteProduct(int? id)
        {
            var Result = Model.DeleteProduct(id);
            return Request.CreateResponse(Result ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
        }
    }
}