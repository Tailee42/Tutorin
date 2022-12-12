﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;

namespace Tutorin.Controllers
{
    public class GestionnaireController : Controller
    {
        public IActionResult Index()
        {
            List<Gestionnaire> listeGestionnaires = new List<Gestionnaire>();
            GestionnaireViewModel gevm;

            using (GestionnaireServices ges = new GestionnaireServices())
            {
                gevm = new GestionnaireViewModel()
                {
                    ListeGestionnaires = ges.ObtientTousLesGestionnaires()

                };

            };

            return View("ListeGestionnaires", gevm);
        }

        [HttpGet]
        public IActionResult Ajouter()
        {
            return View("Ajouter");
        }

        [HttpPost]
        public IActionResult Ajouter(Gestionnaire gestionnaire)
        {
            if (!ModelState.IsValid)
            {
                return View("Ajouter", gestionnaire);
            }

            using (GestionnaireServices ges = new GestionnaireServices())
            {
                ges.CreerGestionnaire(gestionnaire);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Modifier(int gestionnaireId)
        {
            if (gestionnaireId != 0)
            {
                using (GestionnaireServices ges = new GestionnaireServices())
                {
                    Gestionnaire gestionnaire = ges.ObtientTousLesGestionnaires().Where(r => r.Id == gestionnaireId).FirstOrDefault();
                    if (gestionnaire == null)
                    {
                        return View("Error");
                    }
                    return View("Modifier", gestionnaire);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult Modifier(Gestionnaire gestionnaire)
        {
            if (!ModelState.IsValid)
            {
                return View("Modifier", gestionnaire);
            }

            using (GestionnaireServices ges = new GestionnaireServices())
            {
                ges.ModifierGestionnaire(gestionnaire);
                return RedirectToAction("Index");
            }

        }

        public IActionResult Supprimer(int gestionnaireId)
        {
            using (GestionnaireServices ges = new GestionnaireServices())
            {

                ges.SupprimerGestionnaire(gestionnaireId);
                return RedirectToAction("Index");
            }
        }

        public IActionResult TableauDeBord()
        {
            string GestionnaireId = User.FindFirstValue("RoleId");
            Gestionnaire gestionnaire = null;
            int id;

            using (GestionnaireServices gs = new GestionnaireServices())
            {
                if (int.TryParse(GestionnaireId, out id))
                {
                    gestionnaire = gs.TrouverUnGestionnaire(id);
                }
            }

            TableauBordGestionnaireViewModel tbevm = new TableauBordGestionnaireViewModel()
            {
                Gestionnaire = gestionnaire,
            };

            return View("TableauDeBord", tbevm);
        }
    }
}
