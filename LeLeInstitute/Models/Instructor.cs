using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeLeInstitute.Models
{
    public class Instructor
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime HireDate { get; set; }
        public string FullName => $"{LastName} {FirstName}";
        public OfficeAssignment OfficeAssignment { get; set; }
        public ICollection<CourseAssignment> CourseAssignments { get; set; }

    }
}
