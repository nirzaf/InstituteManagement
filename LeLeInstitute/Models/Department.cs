using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LeLeInstitute.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public decimal Budget { get; set; }
        public int? InstructorId { get; set; }
        [Display(Name = "Administrator")] public Instructor Instructor { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}