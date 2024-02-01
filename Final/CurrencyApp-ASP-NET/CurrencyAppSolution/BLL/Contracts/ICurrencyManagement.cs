using Models.DataViewModels.CurrencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface ICurrencyManagement
    {
        IEnumerable<CurrencyDTO> GetAllCurrencies();
        CurrencyDTO GetCurrency(string code);
        List<string> FillDBWithNew(string user);
        bool EditCurrency(string code, string user, CurrencyDTO dt);
    }
}
