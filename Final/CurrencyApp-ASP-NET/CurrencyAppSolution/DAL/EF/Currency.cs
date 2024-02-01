namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Currency
    {
        public int Id { get; set; }

        [Required]
        public string code { get; set; }

        public int quantity { get; set; }

        [Required]
        public string rateFormated { get; set; }

        [Required]
        public string diffFormated { get; set; }

        public double rate { get; set; }

        [Required]
        public string name { get; set; }

        public double diff { get; set; }

        [Column(TypeName = "date")]
        public DateTime date { get; set; }

        [Column(TypeName = "date")]
        public DateTime validFromDate { get; set; }
    }
}
