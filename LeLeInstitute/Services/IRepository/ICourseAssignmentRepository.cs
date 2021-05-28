using System.Collections.Generic;
using System.Threading.Tasks;
using LeLeInstitute.Models;

namespace LeLeInstitute.Services.IRepository
{
    public interface ICourseAssignmentRepository:IRepository<CourseAssignment>
    {
        Task<List<CourseAssignment>> CoursesToInstructorAsync(int id);
        List<CourseAssignment> CoursesToInstructor(int id);
    }
}