using System.Collections.Generic;
using LeLeInstitute.Models;

namespace LeLeInstitute.Services.IRepository
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        IEnumerable<Department> InstructorToDepartments();

        Department InstructorToDepartment(int id);
    }
}