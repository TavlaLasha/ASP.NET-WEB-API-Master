using BLL.Contracts;
using Models.DataViewModels.CurrencyManagement;
using System.Collections.Generic;
using System.Web.Http;

namespace CurrencyServiceAPI.Controllers
{
    public class CurrencyController : ApiController
    {
        readonly ICurrencyManagement currencyManagement;
        public CurrencyController(ICurrencyManagement currencyManagement)
        {
            this.currencyManagement = currencyManagement;
        }

        [Route("api/Currency/FillDBWithLatest/{user}")]
        [HttpGet]
        public List<string> FillDBWithLatest(string user) => currencyManagement.FillDBWithNew(user);

        [Route("api/Currency/EditCurrency/{code}/{user}")]
        [HttpPost]
        public bool EditCurrency(string code, string user, [FromBody] CurrencyDTO u) => currencyManagement.EditCurrency(code, user, u);

        [Route("api/Currency/GetAll")]
        [HttpGet]
        public IEnumerable<CurrencyDTO> GetAllCurrencies() => currencyManagement.GetAllCurrencies();

        [Route("api/Currency/GetCurrency/{code}")]
        [HttpGet]
        public CurrencyDTO GetAllCurrencies(string code) => currencyManagement.GetCurrency(code);
    }
}
