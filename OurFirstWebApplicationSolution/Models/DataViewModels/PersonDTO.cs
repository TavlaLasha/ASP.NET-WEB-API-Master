using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Models.DataViewModels
{
    public class PersonDTO
    {
        [Display(Name = "სახელი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "სახელის მითითება სავალდებულოა"), MaxLength(50, ErrorMessage = "სახელი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string FirstName { get; set; }

        [Display(Name = "გვარი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "გვარის მითითება სავალდებულოა"), MaxLength(50, ErrorMessage = "გვარი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string LastName { get; set; }

        [Display(Name = "პირადი ნომერი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "პირადი ნომრის მითითება სავალდებულოა"), MaxLength(11, ErrorMessage = "პირადი ნომერი არ უნდა აღემატებოდეს 11 სიმბოლოს")]
        public string PN { get; set; }

        [Display(Name = "ელ-ფოსტა")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "შეყვანილი ელ-ფოსტია არავალიდურია")]
        public string Email { get; set; }

        [Display(Name = "დაბადების თარიღი")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "დაბადების თარიღის მითითება სავალდებულოა")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "ფოტო")]
        [DataType(DataType.Upload)]
        public byte[] Photo { get; set; }
    }
}
