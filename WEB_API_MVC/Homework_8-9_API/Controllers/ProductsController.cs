using Homework_8_9_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Http;

namespace Homework_8_9_API.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiControllerBase<ProductsModel>
    {
        #region Constructors
        public ProductsController()
        {
            Model = new ProductsModel();
        }
        #endregion

        [HttpGet]
        [Route("", Name = "GetAllProducts")]
        public HttpResponseMessage Index(int? id = null, int? parentID = null)
        {
            var Result = Model.GetProducts(id, parentID);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetSingleProduct")]
        public HttpResponseMessage GetSingle(int? id)
        {
            var Result = Model.GetSingleProduct(id);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPost]
        [Route("add", Name = "AddProduct")]
        public HttpResponseMessage AddProduct([FromBody] ProductsModel.ProductDTO SubmitModel)
        {
            var Result = Model.AddProduct(SubmitModel);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPut]
        [Route("update", Name = "UpdateProduct")]
        public HttpResponseMessage UpdateProduct([FromBody] ProductsModel.ProductDTO SubmitModel)
        {
            string UserEmail = null;
            if (Request.Headers.Contains("UserEmail"))
            {
                UserEmail = Request.Headers.GetValues("UserEmail").First();
            }

            var Result = Model.UpdateProduct(SubmitModel, UserEmail);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpDelete]
        [Route("delete/{id:int}", Name = "DeleteProduct")]
        public HttpResponseMessage DeleteProduct(int? id)
        {
            var Result = Model.DeleteProduct(id);
            return Request.CreateResponse(Result.StatusCode, Result);
        }
    }
}