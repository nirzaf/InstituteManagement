using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.Models;

namespace LeLeInstitute.ViewModels
{
    public class AssignedCourseData
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public bool Assigned { get; set; }

    }
}
