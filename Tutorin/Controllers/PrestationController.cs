using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;

namespace Tutorin.Controllers
{
    public class PrestationController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            PrestationViewModel pvm = new PrestationViewModel();

            using (PrestationServices ps = new PrestationServices())
            {
                pvm.ListePrestations = ps.ObtientTousLesPrestations();
            }

            return View("ListePrestations", pvm);
        }

        public IActionResult ListeVisiteur()
        {
            PrestationViewModel pvm = new PrestationViewModel();

            using (PrestationServices ps = new PrestationServices())
            {
                pvm.ListePrestations = ps.ObtientToutesLesPrestationsValidees();
            }

            return View("ListeVisiteur", pvm);
        }

        [Authorize (Roles = "ResponsableEleve")]
        public IActionResult VoirPrestationsValidees(int responsableId)
        {
            PrestationViewModel pvm = new PrestationViewModel();

            using (PrestationServices ps = new PrestationServices())
            {
                pvm.ListePrestations = ps.ObtientToutesLesPrestationsValidees();
            }

            return View("ListePrestationsResponsable", pvm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Rechercher(Prestation prestation)
        {
            PrestationViewModel pvm = new PrestationViewModel();

            using (PrestationServices ps = new PrestationServices())
            {
                pvm.ListePrestations = ps.RechercherPrestationsValidees(prestation.TypePrestation, prestation.Niveau);
            }

            return View("ListePrestationsResponsable", pvm);
        }

        [Authorize (Roles = "Enseignant")]
        public IActionResult VoirPrestationsCrees(int enseignantId)
        {
            PrestationViewModel pvm = new PrestationViewModel();

            using (PrestationServices ps = new PrestationServices())
            {
                pvm.ListePrestations = ps.ObtientToutesLesPrestationsCreees();
            }

            return View("ListePrestationsEnseignant", pvm);
        }

        [Authorize(Roles = "Enseignant, Gestionnaire")]
        [HttpGet]
        public IActionResult Ajouter()
        {
            return View("Ajouter");
        }

        [Authorize(Roles = "Enseignant, Gestionnaire")]
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
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Enseignant, Gestionnaire")]
        [HttpGet]
        public IActionResult Modifier(int prestationId)
        {
            if (prestationId != 0)
            {
                Prestation prestation = null;
                using (PrestationServices ps = new PrestationServices())
                {
                    prestation = ps.ObtientTousLesPrestations().Where(r => r.Id == prestationId).FirstOrDefault();
                }

                if (prestation == null)
                {
                    return View("Error");
                }

                return View("Modifier", prestation);
            }
            return View("Error");
        }

        [Authorize(Roles = "Enseignant, Gestionnaire")]
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
            }

            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Enseignant, Gestionnaire")]
        public IActionResult Supprimer(int prestationId)
        {
            using (PrestationServices ps = new PrestationServices())
            {
                ps.SupprimerPrestation(prestationId);
            }

            return RedirectToAction("Index");
        }

        [Authorize (Roles = "ResponsableEleve")]
        public IActionResult InscrireEleve(int prestationId)
        {
            string responsableId = User.FindFirstValue("RoleId");
            int id;
            ResponsableEleve responsable = new ResponsableEleve();
            Prestation prestation = new Prestation();
            List<Eleve> listeEleves = new List<Eleve>();

            using (ResponsableServices rs = new ResponsableServices())
            {
                if (int.TryParse(responsableId, out id))
                {
                    responsable = rs.TrouverUnResponsable(id);
                    listeEleves = rs.TouverLesElevesDUnResponsable(id);
                }
            }

            using (PrestationServices ps = new PrestationServices())
            {
                prestation = ps.TrouverUnePrestation(prestationId);
            }

            PrestationViewModel pvm = new PrestationViewModel()
            {
                ResponsableEleve = responsable,
                Prestation = prestation,
                ListeEleves = listeEleves
            };

            return View("InscrireEleve", pvm);
        }

        [Authorize(Roles = "ResponsableEleve")]
        [HttpPost]
        public IActionResult AjoutEleveAPrestation(int prestationId, List<int> eleveIds)
        {
            Prestation prestation = new Prestation();

            using (PrestationServices ps = new PrestationServices())
            {
                prestation = ps.TrouverUnePrestation(prestationId);

                if (prestation.Prix == 0)
                {
                    foreach (int id in eleveIds)
                        ps.InscrireEleveAPrestation(id, prestationId);

                    return RedirectToAction("TableauDeBord", "ResponsableEleve");

                }
                else
                {
                    PrestationViewModel pvm = new PrestationViewModel() { ElevesId = eleveIds, PrestationId = prestationId };
                    return RedirectToAction("PayerPrestation", "Payement", pvm);
                }

            }
        }


        [Authorize(Roles = "Gestionnaire, Enseignant")]
        public IActionResult VoirPrestationAAffecter()
        {
            List<Prestation> Prestations = new List<Prestation>();
            using (PrestationServices ps = new PrestationServices())
            {
                Prestations = ps.ObtientToutesLesPrestationsCreees();
            }
            PrestationViewModel pvm = new PrestationViewModel()
            {
                ListePrestations = Prestations,
            };

            return View("ListePrestationsEnseignant", pvm);
        }

        [Authorize(Roles = "Gestionnaire, Enseignant")]
        public IActionResult InscrireEnseignant(int prestationId)
        {
            string enseignantId = User.FindFirstValue("RoleId");
            int id;
            Enseignant enseignant = new Enseignant();
            Prestation prestation = new Prestation();

            using (EnseignantServices es = new EnseignantServices())
            {
                if (int.TryParse(enseignantId, out id))
                {
                    enseignant = es.TrouverUnEnseignant(id);
                }
            }

            using (PrestationServices ps = new PrestationServices())
            {
                prestation = ps.TrouverUnePrestationNonAffectee(prestationId);
                ps.InscrireEnseignantAPrestation(id, prestationId);
            }

            PrestationViewModel pvm = new PrestationViewModel()
            {
                Enseignant = enseignant,
                Prestation = prestation
            };

            return RedirectToAction("TableauDeBord", "enseignant");
        }
    }
}
