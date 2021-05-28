using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.DAL;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LeLeInstitute.Services.Repository
{
    public class CourseAssignmentRepository : Repository<CourseAssignment>, ICourseAssignmentRepository
    {
        public CourseAssignmentRepository(LeLeContext leLeContext) : base(leLeContext)
        {
        }

        public async Task<List<CourseAssignment>> CoursesToInstructorAsync(int id)
        {
            return await LeLeContext.CourseAssignments
                .Where(x => x.Id == id)
                .Include(x => x.Course)
                .ToListAsync();
        }

        public List<CourseAssignment> CoursesToInstructor(int id)
        {
            return LeLeContext.CourseAssignments
                .Where(x => x.Id == id)
                .Include(x => x.Course)
                .ToList();
        }
    }

}
