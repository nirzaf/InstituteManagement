using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.Models;

namespace LeLeInstitute.ViewModels
{
    public class EditInstructorViewModel
    {
        public Instructor Instructor { get; set; }
        public List<AssignedCourseData> AssignedCourseData { get; set; }
    }
}
