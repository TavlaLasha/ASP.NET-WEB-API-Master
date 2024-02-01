using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DataViewModels.CurrencyManagement
{
    public class CurrencyDTO
    {
        [Display(Name = "Code")]
        public string code { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Quantity is Required")]
        public int quantity { get; set; }

        [Display(Name = "Rate Formated")]
        [Required(ErrorMessage = "Quantity is Required")]
        public string rateFormated { get; set; }

        [Display(Name = "Diff Formated")]
        [Required]
        public string diffFormated { get; set; }

        [Display(Name = "Rate")]
        [Required]
        public double rate { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string name { get; set; }

        [Display(Name = "Diff")]
        [Required]
        public double diff { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime date { get; set; }

        [Display(Name = "Valid From Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime validFromDate { get; set; }
    }
    public class CurrencyRoot
    {
        public DateTime date { get; set; }
        public List<CurrencyDTO> currencies { get; set; }
    }
}
