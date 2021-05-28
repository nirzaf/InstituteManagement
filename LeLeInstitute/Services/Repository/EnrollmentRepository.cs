using LeLeInstitute.DAL;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;

namespace LeLeInstitute.Services.Repository
{
    public class EnrollmentRepository : Repository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(LeLeContext leLeContext) : base(leLeContext)
        {
        }
    }
}