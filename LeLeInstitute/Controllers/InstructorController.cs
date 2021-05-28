using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using LeLeInstitute.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LeLeInstitute.Controllers
{
    public class InstructorController : Controller
    {
        private readonly ICourseAssignmentRepository _courseAssignmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IInstructorRepository _instructorRepository;

        public InstructorController(IInstructorRepository instructorRepository, ICourseRepository courseRepository,
            ICourseAssignmentRepository courseAssignmentRepository)
        {
            _instructorRepository = instructorRepository;
            _courseRepository = courseRepository;
            _courseAssignmentRepository = courseAssignmentRepository;
        }

        [Route("Instructor/Index/{id:int?}")]
        public async Task<IActionResult> Index(int? id, int? courseId)
        {
            var viewModel = new InstructorViewModel {Instructors = await _instructorRepository.Instructors()};

            if (id != null)
            {
                ViewData["InstructorID"] = id.Value;
                var instructor = viewModel.Instructors.Single(i => i.Id == id.Value);
                viewModel.Courses = instructor.CourseAssignments.Select(s => s.Course);
            }

            if (courseId == null) return View(viewModel);
            ViewData["CourseID"] = courseId.Value;
            viewModel.Enrollments = viewModel.Courses.Single(x => x.Id == courseId).Enrollments;

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var constructor = await _instructorRepository.Instructor((int) id);

            return View(constructor);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var courses = _courseRepository.GetAll();
            var model = new CreateInstructorViewModel
            {
                AssignedCourseData = courses.Select(s => new AssignedCourseData
                {
                    CourseId = s.Id,
                    CourseName = s.CourseName,
                    Assigned = false
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(CreateInstructorViewModel model)
        {
            if (ModelState.IsValid)
            {
                _instructorRepository.Add(model.Instructor);

                var instructorId = model.Instructor.Id;
                var courseAssignment = new List<CourseAssignment>();
                if (model.AssignedCourseData != null)
                    foreach (var data in model.AssignedCourseData)
                        if (data.Assigned)
                            _courseAssignmentRepository.Add(new CourseAssignment {CourseId = data.CourseId, Id = instructorId});
                return RedirectToAction("Index");
            }

            return View("Create");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var instructor = _instructorRepository.GetById(id);
            var allCourses = _courseRepository.GetAll();
            var coursesToInstructor = await _courseAssignmentRepository.CoursesToInstructorAsync(instructor.Id);
            var model = new CreateInstructorViewModel
            {
                Instructor = instructor,
                AssignedCourseData = allCourses.Select(s => new AssignedCourseData
                {
                    CourseId = s.Id,
                    CourseName = s.CourseName,
                    Assigned = coursesToInstructor.Exists(x => x.Course.Id == s.Id)
                }).OrderBy(x => x.CourseName).ToList()
            };


            return View(model);
        }

        [HttpPost]
        [ActionName("Edit")]
        public IActionResult EditPost(CreateInstructorViewModel model)
        {
            if (!ModelState.IsValid) return View("Edit");
            _instructorRepository.Update(model.Instructor);

            var instructorId = model.Instructor.Id;
            if (model.AssignedCourseData == null) return View("Create");
            foreach (var data in model.AssignedCourseData)
            {
                if (data.Assigned)
                {
                    var isExist = IsExist(_courseAssignmentRepository.GetAll(), instructorId, data.CourseId);
                    if (!isExist)
                        _courseAssignmentRepository.Add(new CourseAssignment
                            {CourseId = data.CourseId, Id = instructorId});
                }
                else
                {
                    var isExist = IsExist(_courseAssignmentRepository.GetAll(), instructorId, data.CourseId);

                    if (!isExist) continue;
                    var filter = _courseAssignmentRepository
                        .GetByFiler(x => x.CourseId == data.CourseId && x.Id == instructorId)
                        .FirstOrDefault();
                    _courseAssignmentRepository.Delete(filter);
                }
            }

            return RedirectToAction("Index");

        }

        private bool IsExist(IEnumerable<CourseAssignment> source, int instructorId, int courseId)
        {
            return source.Where(x => x.Id == instructorId).Any(c => c.CourseId == courseId);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                var instructor = await _instructorRepository.Instructor((int) id);
                if (instructor == null) return NotFound();

                return View(instructor);
            }

            return View("Index");
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            if (id != null)
            {
                var instructor = _instructorRepository.GetById((int) id);
                if (instructor == null) return NotFound();

                _instructorRepository.Delete(instructor);
                return RedirectToAction("Index");
            }

            return View("Index");
        }
    }
}