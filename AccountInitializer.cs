using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeLeInstitute.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace LeLeInstitute.DAL
{
    public class AccountInitializer : IAccountInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed(IApplicationBuilder builder)
        {
            using var serviceScope = builder.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<LeLeContext>();



            var adminRole = new IdentityRole("Admin");
            var userRole = new IdentityRole("User");

            if (!_roleManager.Roles.Any())
            {
                var roleList = new List<IdentityRole>() { adminRole, userRole };
                foreach (var identityRole in roleList)
                {
                    await _roleManager.CreateAsync(identityRole);
                }
            }

            if (_userManager.Users.Any()) return;


            var adminUser = new IdentityUser() { UserName = "Karwan", Email = "karwan.essmat@gmail.com" };
            var normalUser = new IdentityUser() { UserName = "sara", Email = "sara@gmail.com" };

            var userList = new List<IdentityUser>() { adminUser, normalUser };
            foreach (var identityUser in userList)
            {
                await _userManager.CreateAsync(identityUser, "Password@123");

            }


            _userManager.AddToRoleAsync(adminUser, adminRole.Name).GetAwaiter().GetResult();
            _userManager.AddToRoleAsync(normalUser, userRole.Name).GetAwaiter().GetResult();
        }
    }


    public interface IAccountInitializer
    {
        Task Seed(IApplicationBuilder builder);
    }
}
