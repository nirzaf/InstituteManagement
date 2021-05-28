using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using LeLeInstitute.Models;
using LeLeInstitute.Services.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LeLeInstitute.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IInstructorRepository _instructorRepository;

        public DepartmentController(IDepartmentRepository departmentRepository, IInstructorRepository instructorRepository)
        {
            _departmentRepository = departmentRepository;
            _instructorRepository = instructorRepository;
        }

        public IActionResult Index()
        {
            var departments = _departmentRepository.InstructorToDepartments();
            return View(departments);
        }

        public IActionResult Details(int detailId)
        {
            var department = _departmentRepository.InstructorToDepartment(detailId);
            if (department ==null)
            {
                return NotFound();
            }

            return View(department);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //InstructorList();
            ViewBag.Instructors = _instructorRepository.GetAll();

            return View();
        }

        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePost(Department model)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Add(model);
            }

            return RedirectToAction("Details", new { detailId = model.Id});
        }

        public void InstructorList()
        {
            ViewBag.Instructors = _instructorRepository.GetAll();
        }

        public IActionResult Edit(int id)
        {
            var department = _departmentRepository.InstructorToDepartment(id);
            if (department ==null)
            {
                return NotFound();
            }

            InstructorList();
            return View(department);
        }

        [HttpPost,ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(Department model)
        {
            if (ModelState.IsValid)
            {
                _departmentRepository.Update(model);
                return RedirectToAction("Details", new { detailId = model.Id});
            }

            return View("Edit");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var department = _departmentRepository.InstructorToDepartment(id);
            return View(department);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department ==null)
            {
                return NotFound();
            }

            _departmentRepository.Delete(department);
            return RedirectToAction("Index");
        }
    }
}