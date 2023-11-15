using Midterm_WEB_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Midterm_MVC_Client.Models
{
    public class ProductDTO
    {
        public int? ID { get; set; }
        [Display(Name = "სახელი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "სახელის მითითება სავალდებულოა"), MaxLength(50, ErrorMessage = "სახელი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string Name { get; set; }

        [Display(Name = "ვადა")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "ვადის მითითება სავალდებულოა")]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}")]
        public DateTime? ExpireDate { get; set; }

        [Display(Name = "დამატების თარიღი")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy HH:mm}")]
        public DateTime? DateAdded { get; set; }
    }

    public class ProductsResponse : ApiResponseBase
    {
        public List<ProductDTO> Products { get; set; }
    }

    public class ProductResponse : ApiResponseBase
    {
        public ProductDTO Product { get; set; }
    }
}