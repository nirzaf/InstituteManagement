using System.Collections.Generic;
using LeLeInstitute.Models;

namespace LeLeInstitute.Services.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<Enrollment> CoursesToStudent(int studentId);
    }
}