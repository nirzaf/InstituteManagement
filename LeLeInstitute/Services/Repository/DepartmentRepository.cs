using System.Collections.Generic;
using System.Linq;
using LeLeInstitute.DAL;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LeLeInstitute.Services.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(LeLeContext leLeContext) : base(leLeContext)
        {
        }

        public IEnumerable<Department> InstructorToDepartments()
        {
            return LeLeContext.Departments.Include(x => x.Instructor).ToList();
        }

        public Department InstructorToDepartment(int id)
        {
            return LeLeContext.Departments.Include(x => x.Instructor).FirstOrDefault(x => x.Id == id);
        }
    }
}