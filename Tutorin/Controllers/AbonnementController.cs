using Microsoft.AspNetCore.Mvc;
using Tutorin.Models;
using Tutorin.Services;

namespace Tutorin.Controllers
{
    public class AbonnementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AjouterEleve(int abonnementId)
        {

            return View("AjouterEleve");
        }

        [HttpPost]
        public IActionResult AjouterEleve(Eleve eleve, int abonnementId)
        {
            if (ModelState.IsValid)
            {
                using (EleveServices es = new EleveServices())
                {
                    es.CreerEleve(eleve);
                }

                using (AbonnementServices abs = new AbonnementServices())
                {
                    abs.AjouterEleve(abonnementId, eleve);
                }

                return RedirectToAction("TableauDeBord", "ResponsableEleve");

            }
            return View("AjouterEleve");
        }
    }
}
