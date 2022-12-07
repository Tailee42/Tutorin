using Microsoft.AspNetCore.Mvc;

namespace Tutorin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
