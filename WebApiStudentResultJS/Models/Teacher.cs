//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiStudentResultJS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Teacher
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Teacher()
        {
            this.CourseTeacherMapings = new HashSet<CourseTeacherMaping>();
        }
    
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherMobile { get; set; }
        public string TeacherEmail { get; set; }
        public string TeacherAddress { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CourseTeacherMaping> CourseTeacherMapings { get; set; }
    }
}
