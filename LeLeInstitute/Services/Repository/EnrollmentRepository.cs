using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.DAL;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;

namespace LeLeInstitute.Services.Repository
{
    public class EnrollmentRepository:Repository<Enrollment>,IEnrollmentRepository
    {
        public EnrollmentRepository(LeLeContext leLeContext) : base(leLeContext)
        {
        }
    }
}
