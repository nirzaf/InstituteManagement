using System.Collections.Generic;
using LeLeInstitute.Models;

namespace LeLeInstitute.Services.IRepository
{
    public interface ICourseRepository : IRepository<Course>
    {
        IEnumerable<Course> CoursesToDepartment();
    }
}