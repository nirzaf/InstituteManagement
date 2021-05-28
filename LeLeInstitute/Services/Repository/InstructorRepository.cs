using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.DAL;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeLeInstitute.Services.Repository
{
    public class InstructorRepository:Repository<Instructor>,IInstructorRepository
    {
        private Repository<Instructor> repository;
        public InstructorRepository(LeLeContext leLeContext) : base(leLeContext)
        {
        }

        public async  Task<IEnumerable<Instructor>> Instructors()
        {
            return await LeLeContext.Instructors
                .Include(x => x.OfficeAssignment)
                .Include(x => x.CourseAssignments)
                    .ThenInclude(x => x.Course)
                        .ThenInclude(x => x.Department)
                .Include(x => x.CourseAssignments)
                    .ThenInclude(x => x.Course)
                        .ThenInclude(x => x.Enrollments)
                            .ThenInclude(x => x.Student)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Instructor> Instructor(int id)
        {
            return await LeLeContext.Instructors
                .Include(x => x.CourseAssignments)
                .ThenInclude(x => x.Course)
                .FirstOrDefaultAsync(i => i.Id == id);
        }


        public void CreateInstructor(Instructor instructor)
        {
            repository.Add(instructor);
        }

        public void UpdateInstructor(Instructor instructor)
        {
            repository.Update(instructor);
        }
    }
}
