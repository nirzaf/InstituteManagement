using System.Collections.Generic;
using LeLeInstitute.Models;

namespace LeLeInstitute.ViewModels
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
        public Enrollment Enrollment { get; set; }
    }
}