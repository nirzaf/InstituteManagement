using System.Linq;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using LeLeInstitute.ViewModels;
using Microsoft.AspNetCore.Mvc;
using ReflectionIT.Mvc.Paging;

namespace LeLeInstitute.Controllers
{
    public class StudentController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository, 
            IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository)
        {
            _studentRepository = studentRepository;
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
        }

        public IActionResult Index(string sortOrder, string searchString, int pageindex = 1)
        {

            ViewData["sortName"] = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["sortByDate"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["currentFilter"] = searchString;

            var students = _studentRepository.GetAll();

            if (!string.IsNullOrEmpty(searchString))
                students = students.Where(s => s.FirstName.ToLower().Contains(searchString.ToLower()) ||
                                               s.LastName.ToLower().Contains(searchString.ToLower()));
            students = sortOrder switch
            {
                "name_desc" => students.OrderByDescending(s => s.FirstName),
                "Date" => students.OrderBy(s => s.EnrollmentDate),
                "date_desc" => students.OrderByDescending(s => s.EnrollmentDate),
                _ => students.OrderBy(s => s.FirstName),
            };
            var model = PagingList.Create(students, 2, pageindex);
            return View(model);
        }

        public IActionResult Details(int id = 0)
        {
            if (id == 0) return NotFound();

            ViewBag.Courses = _courseRepository.GetAll();
            Student student = _studentRepository.GetById(id);
            StudentViewModel model = new StudentViewModel
            {
                Student = student,
                Enrollments = _studentRepository.CoursesToStudent(student.Id)
            };

            return View(model);
        }

        public IActionResult AddCourseToStudent(StudentViewModel model)
        {
            if (!ModelState.IsValid) return RedirectToAction("Details", new {id = model.Enrollment.StudentId});
            if (model.Enrollment.StudentId == 0 || model.Enrollment.CourseId == 0) return RedirectToAction("Index");
            _enrollmentRepository.Add(model.Enrollment);
            return RedirectToAction("Details", new {id = model.Enrollment.StudentId});
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student model)
        {
            if (!ModelState.IsValid) return View("Create");
            _studentRepository.Add(model);
            return RedirectToAction("Index");

        }


        public IActionResult Edit(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null && id == 0) return NotFound();
            return View(student);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Student model)
        {
            if (!ModelState.IsValid) return View("Edit");
            _studentRepository.Update(model);
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null && id == 0) return NotFound();
            return View(student);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var course = _studentRepository.GetById(id);
            if (course == null && id == 0) return NotFound();

            _studentRepository.Delete(course);
            return RedirectToAction("Index");
        }
    }
}