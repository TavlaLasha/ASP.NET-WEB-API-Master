using Midterm_WEB_API.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Midterm_MVC_Client.Models
{
    public class CustomerDTO
    {
        public int? ID { get; set; }
        [Display(Name = "სახელი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "სახელის მითითება სავალდებულოა"), MaxLength(50, ErrorMessage = "სახელი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string Name { get; set; }

        [Display(Name = "ქალაქი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "ქალაქის მითითება სავალდებულოა")]
        public string City { get; set; }
    }

    public class CustomersResponse : ApiResponseBase
    {
        public List<CustomerDTO> Customers { get; set; }
    }

    public class CustomerResponse : ApiResponseBase
    {
        public CustomerDTO Customer { get; set; }
    }
}