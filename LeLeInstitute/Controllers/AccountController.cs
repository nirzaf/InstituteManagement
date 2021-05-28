using System.Threading.Tasks;
using LeLeInstitute.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LeLeInstitute.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return NotFound();

            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var success = await _userManager.CreateAsync(user, model.Password);
            if (success.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("Index");
            }


            foreach (var error in success.Errors) ModelState.AddModelError("", error.Description);

            return View("Register");
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return NotFound();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return NotFound();

            var success = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (success.Succeeded) return RedirectToAction("Index");

            return View("Login");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}