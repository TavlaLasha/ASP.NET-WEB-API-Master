using DAL.EF;
using Models.DataViewModels.CurrencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository.Interfaces
{
    public interface ILogRepository : IDisposable
    {
        IEnumerable<LogDTO> GetAllLogs();
        bool AddLogs(List<CurrencyChangeLog> logs);
        void SaveChanges();
    }
}
