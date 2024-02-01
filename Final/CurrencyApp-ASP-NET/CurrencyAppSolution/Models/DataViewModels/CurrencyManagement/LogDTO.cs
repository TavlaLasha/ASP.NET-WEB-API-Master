using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataViewModels.CurrencyManagement
{
    public class LogDTO
    {
        public int Id { get; set; }
        
        public string CurrencyName { get; set; }
        
        public string User { get; set; }
        
        public string Data { get; set; }
        
        public DateTime Updated_At { get; set; }
    }
}
