using Microsoft.AspNetCore.Mvc;

namespace Tutorin.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
