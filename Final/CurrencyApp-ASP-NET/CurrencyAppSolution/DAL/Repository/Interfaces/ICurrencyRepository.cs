using DAL.EF;
using Models.DataViewModels.CurrencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface ICurrencyRepository : IDisposable
    {
        IEnumerable<CurrencyDTO> GetAllCurrencies();
        CurrencyDTO GetCurrency(string code);
        bool AddCurrencies(List<CurrencyDTO> newCurrencies);
        bool EditCurrencies(Dictionary<string, CurrencyDTO> currencyDict);
        void SaveChanges();

    }
}
