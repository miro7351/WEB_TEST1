using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }

       [Display(Name = "Meno")]
        public string? FirstMidName { get; set; }
       
        [Display(Name ="Priezvisko")]
        public string? LastName { get; set; }

        [Display(Name ="Zápis")]
        public DateTime EnrollmentDate { get; set; }

       // public ICollection<Enrollment> Enrollments { get; set; }
    }
}
