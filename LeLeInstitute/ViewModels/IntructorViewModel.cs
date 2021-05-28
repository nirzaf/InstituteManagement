using System.Collections.Generic;
using LeLeInstitute.Models;

namespace LeLeInstitute.ViewModels
{
    public class InstructorViewModel
    {
        public IEnumerable<Instructor> Instructors { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}