using Homework_8_9_API.Models;
using System.Net.Http;
using System.Web.Http;

namespace Homework_8_9_API.Controllers
{
    [RoutePrefix("api/product-categories")]
    public class ProductCategoriesController : ApiControllerBase<ProductCategoriesModel>
    {
        #region Constructors
        public ProductCategoriesController()
        {
            Model = new ProductCategoriesModel();
        }
        #endregion

        [HttpGet]
        [Route("", Name = "GetAllProductCategories")]
        public HttpResponseMessage Index(int? id = null)
        {
            var Result = Model.GetProductCategories(id);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpGet]
        [Route("{id:int}", Name = "GetSingleProductCategory")]
        public HttpResponseMessage GetSingle(int? id)
        {
            var Result = Model.GetSingleProductCategory(id);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPost]
        [Route("add", Name = "AddProductCategory")]
        public HttpResponseMessage AddProductCategory([FromBody] ProductCategoriesModel.ProductCategoryDTO SubmitModel)
        {
            var Result = Model.AddProductCategory(SubmitModel);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpPut]
        [Route("update", Name = "UpdateProductCategory")]
        public HttpResponseMessage UpdateProductCategory([FromBody] ProductCategoriesModel.ProductCategoryDTO SubmitModel)
        {
            var Result = Model.UpdateProductCategory(SubmitModel);
            return Request.CreateResponse(Result.StatusCode, Result);
        }

        [HttpDelete]
        [Route("delete/{id:int}", Name = "DeleteProductCategory")]
        public HttpResponseMessage DeleteProductCategory(int? id)
        {
            var Result = Model.DeleteProductCategory(id);
            return Request.CreateResponse(Result.StatusCode, Result);
        }
    }
}