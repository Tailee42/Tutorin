using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Security.Claims;
using Tutorin.Models;
using Tutorin.Services;
using System.Data;

namespace Tutorin.Controllers
{
    public class AbonnementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize (Roles = "ResponsableEleve")]
        public IActionResult AjouterEleve(int abonnementId)
        {

            return View("AjouterEleve");
        }

        [Authorize(Roles = "ResponsableEleve")]
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

        [Authorize (Roles = "ResponsableEleve, Gestionnaire")]
        public IActionResult FinAbonnement(int abonnementId)
        {
            using (AbonnementServices abs= new AbonnementServices())
            {
                abs.FinAbonnement(abonnementId);
            }

            return RedirectToAction("TableauDeBord", User.FindFirstValue(ClaimTypes.Role));
        }

        [Authorize (Roles = "ResponsableEleve, Gestionnaire")]
        public IActionResult SupprimerEleve(int abonnementId)
        {

            using (AbonnementServices abs = new AbonnementServices())
            {
                abs.SupprimerEleve(abonnementId);
            }

            return RedirectToAction("TableauDeBord", User.FindFirstValue(ClaimTypes.Role));
        }

        [Authorize (Roles = "ResponsableEleve, Gestionnaire, Eleve")]
        public IActionResult Modifier(int eleveId)
        {
            if (eleveId != 0)
            {
                Eleve eleve = null;
                using (EleveServices es = new EleveServices())
                {
                    eleve = es.ObtientTousLesEleves().Where(r => r.Id == eleveId).FirstOrDefault();
                }

                if (eleve == null)
                {
                    return View("Error");
                }

                return View("Modifier", eleve);
            }
            return View("Error");
        }

        [Authorize (Roles = "ResponsableEleve, Gestionnaire, Eleve")]
        [HttpPost]
        public IActionResult Modifier(Eleve eleve)
        {
            if (!ModelState.IsValid)
            {
                return View("Modifier", eleve);
            }

            using (EleveServices es = new EleveServices())
            {
                es.ModifierEleve(eleve);  
            }

            return RedirectToAction("TableauDeBord", User.FindFirstValue(ClaimTypes.Role));

        }


        [Authorize(Roles = "ResponsableEleve")]
        [HttpGet]
        public IActionResult AjouterAbonnement()
        {
            return View("AjouterAbonnement");
        }

        
    }
}
