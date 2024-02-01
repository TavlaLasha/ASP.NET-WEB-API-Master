namespace DAL.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CurrencyChangeLog")]
    public partial class CurrencyChangeLog
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string CurrencyName { get; set; }

        [Required]
        [StringLength(50)]
        public string User { get; set; }

        [Required]
        public string Data { get; set; }

        [Column(TypeName = "date")]
        public DateTime Updated_At { get; set; }
    }
}
