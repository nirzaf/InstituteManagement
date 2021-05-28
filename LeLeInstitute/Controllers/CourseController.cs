using System.Linq;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace LeLeInstitute.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public CourseController(ICourseRepository courseRepository, IDepartmentRepository departmentRepository)
        {
            _courseRepository = courseRepository;
            _departmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {
            var allCourses = _courseRepository.CoursesToDepartment();
            return View(allCourses);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var course = _courseRepository.CoursesToDepartment().FirstOrDefault(x => x.Id == id);
            if (course == null && id == 0) return NotFound();
            return View(course);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentRepository.GetAll();
            return View();
        }


        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Course model)
        {
            if (!ModelState.IsValid) return View("Create");
            _courseRepository.Add(model);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null) return NotFound();
            ViewBag.Departments = _departmentRepository.GetAll();
            return View(course);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Course model)
        {
            if (!ModelState.IsValid) return View("Edit");
            _courseRepository.Update(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null && id == 0) return NotFound();
            return View(course);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var course = _courseRepository.GetById(id);
            if (course == null && id == 0) return NotFound();
            _courseRepository.Delete(course);
            return RedirectToAction("Index");
        }
    }
}