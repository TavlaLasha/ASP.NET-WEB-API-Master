namespace Homework_8_9_API.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserActionLog
    {
        [Key]
        public int LogID { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }

        [StringLength(200)]
        public string LogActionUrl { get; set; }

        [StringLength(2000)]
        public string LogActionDescription { get; set; }

        public DateTime LogDateCreated { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
