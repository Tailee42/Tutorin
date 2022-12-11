using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult AjouterEleve(int abonnementId)
        {

            return View("AjouterEleve");
        }

        [Authorize]
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

        public IActionResult Supprimer(int abonnementId)
        {
            using (AbonnementServices abs= new AbonnementServices())
            {
                abs.SupprimerAbonnement(abonnementId);
            }

            return RedirectToAction("TableauDeBord", "ResponsableEleve");
        }
    }
}
