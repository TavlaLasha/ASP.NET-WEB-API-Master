using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Homework_8_9.DTOModels
{
    public class ProductCategoryDTO
    {
        public int? ProductCategoryID { get; set; }
        public int? ProductCategoryParentID { get; set; }
        public string ProductCategoryName { get; set; }
    }
}