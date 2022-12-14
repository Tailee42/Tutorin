using Microsoft.AspNetCore.Mvc;

namespace Tutorin.ViewModels
{
    public class TableauDeBordEnseignant : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
