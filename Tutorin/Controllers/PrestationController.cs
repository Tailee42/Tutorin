using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;

namespace Tutorin.Controllers
{
    public class PrestationController : Controller
    {
        public IActionResult Index()
        {
            List<Prestation> listePrestations = new List<Prestation>();
            PrestationViewModel pvm;

            using (PrestationServices ps = new PrestationServices())
            {
                pvm = new PrestationViewModel()
                {
                    ListePrestations = ps.ObtientTousLesPrestations()
                };
            };

            return View("ListePrestations", pvm);
        }

        public IActionResult VoirPrestationsValidees(int responsableId)
        {
            PrestationViewModel pvm;

            using (PrestationServices ps = new PrestationServices())
            {
                pvm = new PrestationViewModel()
                {
                    ListePrestations = ps.ObtientToutesLesPrestationsValidees()
                };
            };

            return View("ListePrestationsCards", pvm);
        }

        [HttpGet]
        public IActionResult Ajouter()
        {
            return View("Ajouter");
        }

        [HttpPost]
        public IActionResult Ajouter(Prestation prestation)
        {
            if (!ModelState.IsValid)
            {
                return View("Ajouter", prestation);
            }

            using (PrestationServices ps = new PrestationServices())
            {
                ps.CreerPrestation(prestation);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Modifier(int prestationId)
        {
            if (prestationId != 0)
            {
                using (PrestationServices ps = new PrestationServices())
                {
                    Prestation prestation = ps.ObtientTousLesPrestations().Where(r => r.Id == prestationId).FirstOrDefault();
                    if (prestation == null)
                    {
                        return View("Error");
                    }

                    return View("Modifier", prestation);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult Modifier(Prestation prestation)
        {
            if (!ModelState.IsValid)
            {
                return View("Modifier", prestation);
            }

            using (PrestationServices ps = new PrestationServices())
            {
                ps.ModifierPrestation(prestation);
                return RedirectToAction("Index");
            }

        }

        public IActionResult Supprimer(int prestationId)
        {
            using (PrestationServices ps = new PrestationServices())
            {
                ps.SupprimerPrestation(prestationId);
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public IActionResult InscrireEleve(ResponsableEleve responsable, int eleveId, int prestationId, int prestationEleveId)
        {
            using (PrestationServices ps = new PrestationServices())
            {
                ps.InscrireEleveAPrestation(eleveId, prestationId, prestationEleveId);
                return RedirectToAction("Index2");
            }

        }
    }
}


