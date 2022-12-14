using Microsoft.AspNetCore.Mvc;
using Tutorin.Services;
using Tutorin.ViewModels;
using System.Collections.Generic;
using Tutorin.Models;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System;
using System.Security.Claims;

namespace Tutorin.Controllers
{
    public class EleveController : Controller
    {
        public IActionResult Index()
        {
            List<Eleve> listeEleves = new List<Eleve>();
            EleveViewModel evm;

            using (EleveServices es = new EleveServices())
            {
                evm = new EleveViewModel()
                {
                    ListeEleves = es.ObtientTousLesEleves()
                };
            };
            return View("ListeEleves", evm);
        }

        [HttpGet]
        public IActionResult Ajouter()
        {
            return View("Ajouter");
        }

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
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Modifier(int eleveId)
        {
            if (eleveId != 0)
            {
                using (EleveServices es = new EleveServices())
                {
                    Eleve eleve = es.ObtientTousLesEleves().Where(r => r.Id == eleveId).FirstOrDefault();
                    if (eleve == null)
                    {
                        return View("Error");
                    }
                    Console.WriteLine(eleve.DateNaissance.ToString());
                    return View("Modifier", eleve);
                }
            }
            return View("Error");
        }

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
                    return RedirectToAction("Index");
                }
            
        }

        public IActionResult Supprimer(int eleveId)
        {
            using(EleveServices es = new EleveServices())
            {
                es.SupprimerEleve(eleveId);
                return RedirectToAction("Index");
            }
        }

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
