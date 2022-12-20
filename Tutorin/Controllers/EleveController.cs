using Microsoft.AspNetCore.Mvc;
using Tutorin.Services;
using Tutorin.ViewModels;
using System.Collections.Generic;
using Tutorin.Models;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Tutorin.Controllers
{
    public class EleveController : Controller
    {
        [Authorize(Roles = "Gestionnaire")]
        public IActionResult Index()
        {
            EleveViewModel evm = new EleveViewModel();

            using (EleveServices es = new EleveServices())
            {
                evm.ListeEleves = es.ObtientTousLesEleves();
            };
            return View("ListeEleves", evm);
        }

        [Authorize(Roles = "ResponsableEleve, Gestionnaire")]
        [HttpGet]
        public IActionResult Ajouter()
        {
            return View("Ajouter");
        }

        [Authorize(Roles = "ResponsableEleve, Gestionnaire")]
        [HttpPost]
        public IActionResult Ajouter(Eleve eleve)
        {
            if (!ModelState.IsValid)
            {
                return View("Ajouter", eleve);
            }

            using (EleveServices es = new EleveServices())
            {
                es.CreerEleve(eleve);  
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ResponsableEleve, Gestionnaire, Eleve")]
        [HttpGet]
        public IActionResult Modifier(int eleveId)
        {
            if (eleveId != 0)
            {
                Eleve eleve = null;
                EleveViewModel evm = new EleveViewModel();
                using (EleveServices es = new EleveServices())
                {
                    eleve = es.ObtientTousLesEleves().Where(r => r.Id == eleveId).FirstOrDefault();
                    evm.Eleve = eleve;
                }

                if (eleve == null)
                {
                    return View("Error");
                }

                return View("Modifier", evm);

            }
            return View("Error");
        }

        [Authorize(Roles = "ResponsableEleve, Gestionnaire, Eleve")]
        [HttpPost]
        public IActionResult Modifier(EleveViewModel evm)
        {
            string role = User.FindFirstValue(ClaimTypes.Role);
            using (EleveServices es = new EleveServices())
            {
                es.ModifierEleve(evm.Eleve.Id, evm.Eleve.Utilisateur.Nom, evm.Eleve.Utilisateur.Prenom, evm.Eleve.Utilisateur.Identifiant, evm.Eleve.DateNaissance, evm.Eleve.Niveau); 
            }

            return RedirectToAction("TableauDeBord", role);
        }

        [Authorize(Roles = "ResponsableEleve, Eleve")]
        [HttpPost]
        public IActionResult ModifierMotdePasse(NewPassword newPassword)
        {
            string eleveId = User.FindFirstValue("RoleId");
            Eleve eleve = null;
            int id;
            EleveViewModel evm = new EleveViewModel();

            using (EleveServices es = new EleveServices())
            {
                if (int.TryParse(eleveId, out id))
                {
                    eleve = es.TrouverUnEleve(id);
                    evm.Eleve = eleve;
                    es.ModifierMotdePasse(eleve, newPassword.OldPassword, newPassword.NouveauPassword, newPassword.ConfirmPassword);
                }
            }

            return RedirectToAction("TableauDeBord", "eleve");
        }


        [Authorize(Roles = "ResponsableEleve, Gestionnaire")]
        public IActionResult Supprimer(int eleveId)
        {
            using(EleveServices es = new EleveServices())
            {
                es.SupprimerEleve(eleveId);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Eleve")]
        public IActionResult TableauDeBord()
        {
            string eleveId = User.FindFirstValue("RoleId");
            Eleve eleve = null;
            int id;

            using (EleveServices es = new EleveServices())
            {
                if (int.TryParse(eleveId, out id))
                {
                    eleve = es.TrouverUnEleve(id);
                }
            }

            using (PrestationServices pp = new PrestationServices())
            {
                eleve.Prestations = pp.TouverLesPrestationsDUnEleve(id);
            }

            Abonnement abonnement = null;
            using (AbonnementServices abs = new AbonnementServices())
            {
                abonnement = abs.TrouverAbonnementEleve(id);
            }

            TableauBordEleveViewModel tbevm = new TableauBordEleveViewModel()
            {
                Eleve = eleve, Abonnement = abonnement
            };

            return View("TableauDeBord", tbevm);
        }
    }
}
