using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

                return RedirectToAction("TableauDeBord", User.FindFirstValue(ClaimTypes.Role));

            }
            return View("AjouterEleve");
        }

        [Authorize]
        public IActionResult FinAbonnement(int abonnementId)
        {
            using (AbonnementServices abs= new AbonnementServices())
            {
                abs.FinAbonnement(abonnementId);
            }

            return RedirectToAction("TableauDeBord", User.FindFirstValue(ClaimTypes.Role));
        }

        [Authorize]
        public IActionResult SupprimerEleve(int abonnementId)
        {

            using (AbonnementServices abs = new AbonnementServices())
            {
                abs.SupprimerEleve(abonnementId);
            }

            return RedirectToAction("TableauDeBord", User.FindFirstValue(ClaimTypes.Role));
        }

        [Authorize]
        [HttpGet]
        public IActionResult AjouterAbonnement()
        {
            return View("Ajouter");
        }

        [Authorize]
        [HttpPost]
        public IActionResult AjouterAbonnement(Abonnement abonnement)
        {
            if (!ModelState.IsValid)
            {
                return View("AjouterAbonnement", abonnement);
            }

            using (AbonnementServices abs = new AbonnementServices())
            {
                abs.CreerAbonnement(abonnement);
                return RedirectToAction("TableauDeBord", User.FindFirstValue(ClaimTypes.Role));
            }
        }
    }
}
