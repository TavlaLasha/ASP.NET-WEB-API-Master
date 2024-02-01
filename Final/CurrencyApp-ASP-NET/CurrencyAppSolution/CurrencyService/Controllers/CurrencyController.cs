using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BLL.Contracts;
using Models.DataViewModels.CurrencyManagement;

namespace CurrencyService.Controllers
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
