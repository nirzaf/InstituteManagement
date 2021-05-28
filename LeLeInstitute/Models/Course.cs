using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LeLeInstitute.Models
{
    public class Course
    {
        public int Id { get; set; }
        [Display(Name = "Course Title")]
        [Required(ErrorMessage = "Please fill the course Title")]
        public string CourseName { get; set; }
        public int Credits { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
        public ICollection<CourseAssignment> CourseAssignments { get; set; }
    }
}
