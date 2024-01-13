using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework_8_9.DTOModels
{
    public class ProductDTO
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public string City { get; set; }
        public DateTime? ExpireDate { get; set; }
        public DateTime? DateAdded { get; set; }
    }
}