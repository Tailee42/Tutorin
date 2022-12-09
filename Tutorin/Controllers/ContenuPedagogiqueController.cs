using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;

namespace Tutorin.Controllers
{
    public class ContenuPedagogiqueController : Controller
    {
        public IActionResult Index()
        {
            List<ContenuPedagogique> listeCours = new List<ContenuPedagogique>();
            ContenuPedagogiqueViewModel cpvm;

            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                cpvm = new ContenuPedagogiqueViewModel()
                {
                    ListeContenusPedagogiques = cps.ObtenirTousLesContenusPedagogiques()
                };
            };

            return View("ListeContenusPedagogiques", cpvm);
        }

        [HttpPost] 
        public IActionResult RechercherNiveau(TypeNiveau niveau)
        {
            List<ContenuPedagogique> listeCours = new List<ContenuPedagogique>();
            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                listeCours = cps.RechercherCoursParNiveau(niveau);

            };
            return View("ListeContenusPedagogiques", listeCours);
        }
            

        [HttpGet]
        public IActionResult Ajouter()
        {
            return View("Ajouter");
        }

        [HttpPost]
        public IActionResult Ajouter(ContenuPedagogique cours)
        {
            if (!ModelState.IsValid)
            {
                return View("Ajouter", cours);
            }

            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                cps.CreerContenuPedagogique(cours);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Modifier(int coursId)
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

                    return View("Modifier", cours);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult Modifier(ContenuPedagogique cours)
        {
            if (!ModelState.IsValid)
            {
                return View("Modifier", cours);
            }

            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                cps.ModifierContenuPedagogique(cours);
                return RedirectToAction("Index");
            }

        }

        public IActionResult Supprimer(int coursId)
        {
            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                cps.SupprimerContenuPedagogique(coursId);
                return RedirectToAction("Index");
            }
        }


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
