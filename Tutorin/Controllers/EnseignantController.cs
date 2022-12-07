using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;

namespace Tutorin.Controllers
{
    public class EnseignantController : Controller
    {
        public IActionResult Index()
        {
            List<Enseignant> listeEnseignants = new List<Enseignant>();
            EnseignantViewModel envm;

            using (EnseignantServices ens = new EnseignantServices())
            {
                envm = new EnseignantViewModel()
                {
                    ListeEnseignants = ens.ObtientTousLesEnseignants()

                };

            };

            return View("ListeEnseignants", envm);
        }

        [HttpGet]
        public IActionResult Ajouter()
        {
            return View("Ajouter");
        }

        [HttpPost]
        public IActionResult Ajouter(Enseignant enseignant)
        {
            if (!ModelState.IsValid)
            {
                return View("Ajouter", enseignant);
            }

            using (EnseignantServices en = new EnseignantServices())
            {
                en.CreerEnseignant(enseignant);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Modifier(int enseignantId)
        {
            if (enseignantId != 0)
            {
                using (EnseignantServices ens = new EnseignantServices())
                {
                    Enseignant enseignant = ens.ObtientTousLesEnseignants().Where(r => r.Id == enseignantId).FirstOrDefault();
                    if (enseignant == null)
                    {
                        return View("Error");
                    }
                    return View("Modifier", enseignant);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult Modifier(Enseignant enseignant)
        {
            if (!ModelState.IsValid)
            {
                return View("Modifier", enseignant);
            }

            using (EnseignantServices ens = new EnseignantServices())
            {
                ens.ModifierEnseignant(enseignant);
                return RedirectToAction("Index");
            }

        }

        public IActionResult Supprimer(int enseignantId)
        {
            using (EnseignantServices ens = new EnseignantServices())
            {
                ens.SupprimerEnseignant(enseignantId);
                return RedirectToAction("Index");
            }
        }
    }


}
