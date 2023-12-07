namespace WebApplicationService.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Curriculum")]
    public partial class Curriculum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curriculum()
        {
            Courses = new HashSet<Cours>();
        }

        public int ID { get; set; }

        public int SubjectID { get; set; }

        public int LecturerID { get; set; }

        public byte[] Syllabus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cours> Courses { get; set; }

        public virtual Lecturer Lecturer { get; set; }

        public virtual Subject Subject { get; set; }
    }
}
