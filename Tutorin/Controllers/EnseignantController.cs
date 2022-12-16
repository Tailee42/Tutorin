using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;

namespace Tutorin.Controllers
{
    public class EnseignantController : Controller
    {
        public IActionResult Index()
        {
            EnseignantViewModel envm = new EnseignantViewModel();

            using (EnseignantServices ens = new EnseignantServices())
            {
                envm.ListeEnseignants = ens.ObtientTousLesEnseignants();
            };

            return View("ListeEnseignants", envm);
        }

        public IActionResult ListeVisiteur()
        {
            EnseignantViewModel evm = new EnseignantViewModel();

            using (EnseignantServices es = new EnseignantServices())
            {
                evm.ListeEnseignants = es.ObtientTousLesEnseignants();
            };

            return View("ListeVisiteur", evm);
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
                return RedirectToAction("Index", "Login");
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
            string role = User.FindFirstValue(ClaimTypes.Role);
            using (EnseignantServices ens = new EnseignantServices())
            {
                ens.ModifierEnseignant(enseignant);
                return RedirectToAction("TableauDeBord", role);
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

        public IActionResult TableauDeBord()
        {
            string enseignantId = User.FindFirstValue("RoleId");
            Enseignant enseignant = null;
            int id;
            List<Enseignant> enseignants = new List<Enseignant>();
            List<Prestation> prestations = new List<Prestation>();
            
           
            EnseignantViewModel envm = new EnseignantViewModel( );

            using (EnseignantServices ens = new EnseignantServices())
            {
                if (int.TryParse(enseignantId, out id))
                {
                    enseignant = ens.TrouverUnEnseignant(id);
                }
            }

            using (PrestationServices prs = new PrestationServices())
            {
                enseignant.Prestations = prs.TrouverPrestations(id);
                prestations = prs.ObtientTousLesPrestations();
                envm.Prestations = prestations;

            }

            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                enseignant.ContenuPedagogiques = cps.TrouverLesCours(id);
            }
            envm.Enseignant = enseignant;

           
            return View("TableauDeBord", envm);

        }

        
       
        
    }


}
