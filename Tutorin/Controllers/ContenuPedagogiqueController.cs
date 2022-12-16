using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;

namespace Tutorin.Controllers
{
    public class ContenuPedagogiqueController : Controller
    {
        [Authorize (Roles = "Eleve, Gestionaire")]
        public IActionResult Index()
        {
            ContenuPedagogiqueViewModel cpvm = new ContenuPedagogiqueViewModel();

            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                cpvm.ListeContenusPedagogiques = cps.ObtenirTousLesContenusPedagogiquesValides();
            };

            return View("ListeContenusPedagogiques", cpvm);
        }

        [Authorize (Roles = "Gestionnaire")] 
        public IActionResult TableauContenuPedagogique()
        {
            ContenuPedagogiqueViewModel cpvm = new ContenuPedagogiqueViewModel();

            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                cpvm.ListeContenusPedagogiques = cps.ObtenirTousLesContenusPedagogiques();
            };

            return View("TableauContenusPedagogiques", cpvm);
        }

        [Authorize]
        [HttpPost] 
        public IActionResult Rechercher(TypeNiveau niveau, TypeMatiere matiere)
        {
            ContenuPedagogiqueViewModel cpvm = new ContenuPedagogiqueViewModel();

            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                cpvm.ListeContenusPedagogiques = cps.RechercherCours(niveau, matiere);
            };

            return View("ListeContenusPedagogiques", cpvm);
        }

        [Authorize (Roles = "Enseignant")]
        [HttpGet]
        public IActionResult Ajouter()
        {
            return View("Ajouter");
        }

        [Authorize(Roles = "Enseignant")]
        [HttpPost]
        public IActionResult Ajouter(ContenuPedagogique cours)
        {
            if (!ModelState.IsValid)
            {
                return View("Ajouter", cours);
            }

            string enseignantId = User.FindFirstValue("RoleId");

            if (int.TryParse(enseignantId, out int id))
            {
                cours.EnseignantId = id;
            }

            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                cps.CreerContenuPedagogique(cours);
            }

            return RedirectToAction("TableauDeBord", "enseignant");
        }

        [Authorize (Roles = "Enseignant")]
        [HttpGet]
        public IActionResult Modifier(int coursId)
        {
            if (coursId != 0)
            {
                ContenuPedagogique cours = null;
                using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
                {
                    cps.ObtenirTousLesContenusPedagogiques().Where(c => c.Id == coursId).FirstOrDefault();
                }

                if (cours == null)
                {
                    return View("Error");
                }

                return View("Modifier", cours);
            }
            return View("Error");
        }

        [Authorize (Roles = "Enseignant")]
        [HttpPost]
        public IActionResult Modifier(ContenuPedagogique cours)
        {
            if (!ModelState.IsValid)
            {
                return View("Modifier", cours);
            }
            string enseignantId = User.FindFirstValue("RoleId");

            if (int.TryParse(enseignantId, out int id))
            {
                cours.EnseignantId = id;
            }

            string role = User.FindFirstValue(ClaimTypes.Role);

            if(role == "Enseignant") {
                cours.Etat = EtatContenuPedagogique.A_Valider;
            }

            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                cps.ModifierContenuPedagogique(cours);
            }

            return RedirectToAction("TableauDeBord", role);

        }

        [Authorize (Roles = "Gestionnaire")]
        public IActionResult Supprimer(int coursId)
        {
            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                cps.SupprimerContenuPedagogique(coursId);
            }

            return RedirectToAction("TableauContenuPedagogique");
        }

        [Authorize (Roles = "Enseignant, Eleve")]
        public IActionResult Afficher(int coursId)
        {
            if (coursId != 0)
            {
                using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
                {
                    ContenuPedagogique cours = cps.ObtenirTousLesContenusPedagogiques().Where(c => c.Id == coursId).FirstOrDefault();
                    if (cours == null)
                    {
                        return View("Error");
                    } 
                    return View("Afficher", cours);
                }
            }
            return View("Error");
        }

    }
    
}
