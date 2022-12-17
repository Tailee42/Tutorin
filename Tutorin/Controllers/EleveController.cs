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

        [Authorize(Roles = "ResponsableEleve, Gestionnaire, Eleve")]
        [HttpPost]
        public IActionResult Modifier(Eleve eleve)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View("Modifier", eleve);
            //}

            string role = User.FindFirstValue(ClaimTypes.Role);
            using (EleveServices es = new EleveServices())
            {
                es.ModifierEleve(eleve.Id, eleve.Utilisateur.Nom, eleve.Utilisateur.Prenom, eleve.Utilisateur.Identifiant, eleve.DateNaissance, eleve.Niveau) ; 
            }

            return RedirectToAction("TableauDeBord", role);
        }

        [Authorize(Roles = "ResponsableEleve, Gestionnaire, Eleve")]
        [HttpPost]
        public IActionResult ModifierMotdePasse(string ancienMdp, string newMdp, string confirmMdp)
        {
            string eleveId = User.FindFirstValue("RoleId");
            Eleve eleve = null;
            int id;

            using (EleveServices es = new EleveServices())
            {
                if (int.TryParse(eleveId, out id))
                {
                    eleve = es.TrouverUnEleve(id);
                    es.ModifierMotdePasse(eleve, ancienMdp, newMdp, confirmMdp);
                }
            }
            return View("Modifier", eleve);
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
