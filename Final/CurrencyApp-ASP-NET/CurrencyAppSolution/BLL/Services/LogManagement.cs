using BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DataViewModels.CurrencyManagement;
using DAL.Repository;
using DAL;
using DAL.EF;

namespace BLL.Services
{
    public class LogManagement : ILogManagement
    {
        public IEnumerable<LogDTO> GetAllLogs()
        {
            UnitOfWork _unitOfWork = new UnitOfWork(new CurrencyDBContext());
            return _unitOfWork.LogRepo.GetAllLogs();
        }
    }
}
