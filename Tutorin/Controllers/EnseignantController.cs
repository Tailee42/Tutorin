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
    public class EnseignantController : Controller
    {
        [Authorize (Roles = "Gestionnaire")]
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
            }

            return RedirectToAction("Index", "Login");
        }

        [Authorize(Roles = "Gestionnaire, Enseignant")]
        [HttpGet]
        public IActionResult Modifier(int enseignantId)
        {
            if (enseignantId != 0)
            {
                Enseignant enseignant = null;
                EnseignantViewModel envm = new EnseignantViewModel();
                using (EnseignantServices ens = new EnseignantServices())
                {
                    enseignant = ens.ObtientTousLesEnseignants().Where(r => r.Id == enseignantId).FirstOrDefault();
                    envm.Enseignant = enseignant;
                }

                if (enseignant == null)
                {
                    return View("Error");
                }
                return View("Modifier", envm);
            }
            return View("Error");
        }

        [Authorize(Roles = "Gestionnaire, Enseignant")]
        [HttpPost]
        public IActionResult Modifier(EnseignantViewModel envm)
        {
            //le model state devient false depuis l'ajout de la méthode modifier un mot de passe
            //if (!ModelState.IsValid)
            //{
            //    return View("Modifier", enseignant);
            //}

            string role = User.FindFirstValue(ClaimTypes.Role);

            using (EnseignantServices ens = new EnseignantServices())
            {
                ens.ModifierEnseignant(envm.Enseignant.Id, envm.Enseignant.Utilisateur.Nom, envm.Enseignant.Utilisateur.Prenom, envm.Enseignant.Utilisateur.Identifiant, envm.Enseignant.Matiere, envm.Enseignant.Niveaux);
            }

            return RedirectToAction("TableauDeBord", role);
        }

        [Authorize(Roles = "Enseignant")]
        [HttpPost]
        public IActionResult ModifierMotdePasse(NewPassword newPassword)
        {
            string enseignantId = User.FindFirstValue("RoleId");
            Enseignant enseignant = null;
            int id;
            EnseignantViewModel envm = new EnseignantViewModel();

            using (EnseignantServices ens = new EnseignantServices())
            {
                if (int.TryParse(enseignantId, out id))
                {
                    enseignant = ens.TrouverUnEnseignant(id);
                    envm.Enseignant = enseignant;
                    ens.ModifierMotdePasse(enseignant, newPassword.OldPassword, newPassword.NouveauPassword, newPassword.ConfirmPassword);
                }
            }

            return RedirectToAction("TableauDeBord", "enseignant");
        }

        [Authorize(Roles = "Gestionnaire, Enseignant")]
        public IActionResult Supprimer(int enseignantId)
        {
            using (EnseignantServices ens = new EnseignantServices())
            {
                ens.SupprimerEnseignant(enseignantId);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Enseignant")]
        public IActionResult TableauDeBord()
        {
            string enseignantId = User.FindFirstValue("RoleId");
            Enseignant enseignant = null;
            int id; 
           
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
                envm.Prestations = prs.ObtientTousLesPrestations();

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
