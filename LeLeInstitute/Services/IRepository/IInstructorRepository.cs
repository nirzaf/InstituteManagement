using System.Collections.Generic;
using System.Threading.Tasks;
using LeLeInstitute.Models;

namespace LeLeInstitute.Services.IRepository
{
    public interface IInstructorRepository:IRepository<Instructor>
    {

       Task<IEnumerable<Instructor>> Instructors();
       Task<Instructor> Instructor(int id);
       void CreateInstructor(Instructor instructor);
       void UpdateInstructor(Instructor instructor);

    }
}