using Microsoft.AspNetCore.Mvc;

namespace LeLeInstitute.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}