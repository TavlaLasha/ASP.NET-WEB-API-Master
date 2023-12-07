using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationService.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class UserViewModel
    {
        [JsonProperty(PropertyName = "FirstName")]
        [Display(Name="სახელი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage ="სახელის მითითება სავალდებულოა"), MaxLength(50, ErrorMessage ="სახელი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "LastName")]
        [Display(Name = "გვარი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "გვარის მითითება სავალდებულოა"), MaxLength(50, ErrorMessage = "გვარი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "IDNumber")]
        [Display(Name = "პირადი ნომერი")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "პირადი ნომრის მითითება სავალდებულოა"), MaxLength(11, ErrorMessage = "პირადი ნომერი არ უნდა აღემატებოდეს 11 სიმბოლოს")]
        [RegularExpression(@"(\S\W)+", ErrorMessage = "Space and characters not allowed")]
        public string IDNumber { get; set; }

        [JsonProperty(PropertyName = "PhoneNumber")]
        [Display(Name = "ტელეფონი")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(9)]
        public string PhoneNumber { get; set; }

        [JsonProperty(PropertyName = "Email")]
        [Display(Name = "ელ-ფოსტა")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "ელ-ფოსტის მითითება სავალდებულოა")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "შეყვანილი ელ-ფოსტია არავალიდურია")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "Password")]
        [Display(Name = "პაროლი")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "პაროლის მითითება სავალდებულოა"), MaxLength(50, ErrorMessage = "პაროლი არ უნდა აღემატებოდეს 50 სიმბოლოს")]
        public string Password { get; set; }
    }
}