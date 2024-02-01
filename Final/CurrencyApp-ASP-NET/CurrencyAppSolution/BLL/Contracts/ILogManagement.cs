using Models.DataViewModels.CurrencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contracts
{
    public interface ILogManagement
    {
        IEnumerable<LogDTO> GetAllLogs();
    }
}
