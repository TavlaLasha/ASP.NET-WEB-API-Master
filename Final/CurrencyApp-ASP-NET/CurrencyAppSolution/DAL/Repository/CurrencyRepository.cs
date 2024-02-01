using DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using Models.DataViewModels.CurrencyManagement;

namespace DAL.Repository
{
    public class CurrencyRepository : ICurrencyRepository, IDisposable
    {
        private CurrencyDBContext _db;

        public CurrencyRepository(CurrencyDBContext db)
        {
            _db = db;
        }

        public bool AddCurrencies(List<CurrencyDTO> newCurrencies)
        {
            try
            {
                if (!newCurrencies.Any())
                    return false;

                List<Currency> curs = new List<Currency>();
                foreach (CurrencyDTO cdt in newCurrencies)
                {
                    curs.Add(new Currency
                    {
                        code = cdt.code,
                        quantity = cdt.quantity,
                        rateFormated = cdt.rateFormated,
                        diffFormated = cdt.diffFormated,
                        rate = cdt.rate,
                        name = cdt.name,
                        diff = cdt.diff,
                        date = cdt.date,
                        validFromDate = cdt.validFromDate
                    });
                }

                _db.Currencies.AddRange(curs);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditCurrencies(Dictionary<string, CurrencyDTO> currencyDict)
        {
            try
            {
                foreach (KeyValuePair<string, CurrencyDTO> kvp in currencyDict)
                {
                    var curr = _db.Currencies.Where(i => i.code.Equals(kvp.Key)).First();

                    curr.quantity = kvp.Value.quantity;
                    curr.rateFormated = kvp.Value.rateFormated;
                    curr.diffFormated = kvp.Value.diffFormated;
                    curr.rate = kvp.Value.rate;
                    curr.name = kvp.Value.name;
                    curr.diff = kvp.Value.diff;
                    curr.date = kvp.Value.date;
                    curr.validFromDate = kvp.Value.validFromDate;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<CurrencyDTO> GetAllCurrencies()
        {
            return _db.Currencies.Select(i => new CurrencyDTO
            {
                code = i.code,
                quantity = i.quantity,
                rateFormated = i.rateFormated,
                diffFormated = i.diffFormated,
                rate = i.rate,
                name = i.name,
                diff = i.diff,
                date = i.date,
                validFromDate = i.validFromDate
            });
        }

        public CurrencyDTO GetCurrency(string code)
        {
            return _db.Currencies.Where(i => i.code.Equals(code)).Select(i => new CurrencyDTO
            {
                code = i.code,
                quantity = i.quantity,
                rateFormated = i.rateFormated,
                diffFormated = i.diffFormated,
                rate = i.rate,
                name = i.name,
                diff = i.diff,
                date = i.date,
                validFromDate = i.validFromDate
            }).FirstOrDefault();
        }

        public void SaveChanges()
        {
            _db.SaveChanges();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CurrencyRepository() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion

    }
}
