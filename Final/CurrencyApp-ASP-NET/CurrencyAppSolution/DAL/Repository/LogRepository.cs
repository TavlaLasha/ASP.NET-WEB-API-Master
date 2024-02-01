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
    public class LogRepository : ILogRepository, IDisposable
    {
        private CurrencyDBContext _db;
        public LogRepository(CurrencyDBContext db)
        {
            _db = db;
        }

        public bool AddLogs(List<CurrencyChangeLog> logs)
        {
            try
            {
                if (!logs.Any())
                    return false;

                _db.CurrencyChangeLogs.AddRange(logs);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<LogDTO> GetAllLogs()
        {
            return _db.CurrencyChangeLogs.Select(i => new LogDTO
            {
                Id = i.Id,
                CurrencyName = i.CurrencyName,
                User = i.User,
                Data = i.Data,
                Updated_At = i.Updated_At
            }).OrderByDescending(Item => Item.Updated_At);
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
        // ~LogRepository() {
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
