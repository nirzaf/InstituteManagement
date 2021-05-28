using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeLeInstitute.Controllers
{


    [Authorize(Policy = "OnlyAdmin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.Select(s => new RoleViewModel
            {
                Id = s.Id,
                Name = s.Name
            }).ToListAsync();

            return View(roles);
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            var model = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> CreatePost(RoleViewModel model)
        {
            if (model==null)
            {
                return NotFound();
            }
            if (await _roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("", "Name is exist");
            }
            var role = new IdentityRole()
            {
                Name = model.Name
            };

            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id==null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            var model = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(model);
        }


        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditPost(RoleViewModel model)
        {
            if (model==null)
            {
                return NotFound();
            }

            if (await  _roleManager.RoleExistsAsync(model.Name))
            {
                ModelState.AddModelError("","Name is exist");
            }

            var role = await _roleManager.FindByIdAsync(model.Id);
            role.Name = model.Name;
            role.NormalizedName = model.Name.ToUpper();
            await _roleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            var model = new RoleViewModel()
            {
                Id = role.Id,
                Name = role.Name
            };
            return View(model);
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            await _roleManager.DeleteAsync(role);

            return RedirectToAction("Index");
        }


    }
}