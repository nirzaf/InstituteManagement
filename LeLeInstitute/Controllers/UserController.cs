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

    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.Select(s => new UserViewModel
            {
                Id = s.Id,
                Email = s.Email
            }).ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = (await _userManager.GetRolesAsync(user)).ToList();  // karwan ==> Admin and user || lewan ==>user

            var model = new UserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                UserInRoles = roles.Select(s=>new RoleViewModel()
                {
                    Id = s.Id,
                    Name = s.Name,
                    Select = userRoles.Exists(x=>x==s.Name)
                }).ToList()
            };

            return View(model);
        }

        [Authorize(Policy = "OnlyAdmin")]
        [HttpPost,ActionName("Details")]
        public async Task<IActionResult> Update(string id, UserViewModel model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            var roles = await _roleManager.Roles.ToListAsync();

            foreach (var role in model.UserInRoles)
            {

                if (role.Select)
                {
                    await _userManager.AddToRoleAsync(user, roles.FirstOrDefault(x => x.Id == role.Id)?.Name);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, roles.FirstOrDefault(x => x.Id == role.Id)?.Name);
                }
            }

            return RedirectToAction("Index");
        }

        //[HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);

            return RedirectToAction("Index");
        }
    }
}