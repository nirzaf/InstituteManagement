using System.Collections.Generic;
using LeLeInstitute.Models;

namespace LeLeInstitute.ViewModels
{
    public class CreateInstructorViewModel
    {
        public Instructor Instructor { get; set; }
        public List<AssignedCourseData> AssignedCourseData { get; set; }
    }
}