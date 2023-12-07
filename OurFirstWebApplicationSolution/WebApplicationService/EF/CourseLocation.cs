namespace WebApplicationService.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseLocation")]
    public partial class CourseLocation
    {
        public int ID { get; set; }

        public int LocationID { get; set; }

        public string Description { get; set; }

        [StringLength(200)]
        public string Contact { get; set; }

        public int CourseStreamID { get; set; }

        public virtual CourseStream CourseStream { get; set; }

        public virtual Location Location { get; set; }
    }
}
