//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Lesson_4.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Curriculum
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Curriculum()
        {
            this.Courses = new HashSet<Cours>();
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
