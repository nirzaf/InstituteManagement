using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeLeInstitute.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Display(Name = "First Name")] public string FirstName { get; set; }

        [Display(Name = "Last Name")] public string LastName { get; set; }

        public Gender Gender { get; set; }

        [Display(Name = "Enrollment Date")]
        [DisplayFormat(DataFormatString = "{0:dd,MM,yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }


        public ICollection<Enrollment> Enrollments { get; set; }
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}