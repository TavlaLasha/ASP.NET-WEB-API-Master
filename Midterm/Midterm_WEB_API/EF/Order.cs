//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Midterm_WEB_API.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        public int ID { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> Total { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> ProductID { get; set; }
        public Nullable<System.DateTime> OrderDate { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}