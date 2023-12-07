namespace WebApplicationService.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Registration
    {
        public int ID { get; set; }

        public int StudentID { get; set; }

        public int CourseStreamID { get; set; }

        [Column(TypeName = "date")]
        public DateTime RegistrationDate { get; set; }

        public bool IsApproved { get; set; }

        public virtual CourseStream CourseStream { get; set; }

        public virtual User User { get; set; }
    }
}
