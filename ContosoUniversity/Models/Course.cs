using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name ="Identifikátor predmetu")]
        public int CourseID { get; set; }
        [Display(Name ="Názov predmetu")]
        public string Title { get; set; }
       [Display(Name ="Počet kreditov")]
        public int Credits { get; set; }
        [Display(Name ="Zoznam zápisov")]
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
