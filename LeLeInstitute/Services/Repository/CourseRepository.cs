using System.Collections.Generic;
using System.Linq;
using LeLeInstitute.DAL;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LeLeInstitute.Services.Repository
{
    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        public CourseRepository(LeLeContext leLeContext) : base(leLeContext)
        {
        }

        public IEnumerable<Course> CoursesToDepartment()
        {
            return LeLeContext.Courses.Include(x => x.Department).ToList();
        }
    }
}