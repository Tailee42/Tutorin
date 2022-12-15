using Microsoft.AspNetCore.Mvc;
using Tutorin.Services;
using System.Collections.Generic;
using Tutorin.Models;
using System.Linq;
using Tutorin.ViewModels;
using System.Security.Claims;

namespace Tutorin.Controllers
{
    public class ResponsableEleveController : Controller
    {
        public IActionResult Index()
        {
            List<ResponsableEleve> listeResponsable = new List<ResponsableEleve>();
            ResponsableEleveViewModel revm;

            using (ResponsableServices rs = new ResponsableServices())
            {
                revm = new ResponsableEleveViewModel()
                {
                    ListeResponsablesEleves = rs.ObtenirTousLesResponsables()
                };
            };
                return View("ListeResponsablesEleves", revm);
        }

        [HttpGet]
        public IActionResult Ajouter()
        {
            return View("Ajouter");
        }

        [HttpPost]
        public IActionResult Ajouter(ResponsableEleve responsable)
        {
            if (!ModelState.IsValid)
            {
                return View("Ajouter", responsable);
            }

            using (ResponsableServices rs = new ResponsableServices())
            {
                rs.CreerResponsable(responsable);
                return RedirectToAction("Index","Login");
            }
        }

        [HttpGet]
        public IActionResult Modifier(int responsableId)
        {
            if (responsableId != 0)
            {
                using (ResponsableServices rs = new ResponsableServices())
                {
                    ResponsableEleve responsable = rs.ObtenirTousLesResponsables().Where(r => r.Id == responsableId).FirstOrDefault();
                    if (responsable == null)
                    {
                        return View("Error");
                    }
                    return View("Modifier", responsable);
                }
            }
            return View("Error");
        }

        [HttpPost]
        public IActionResult Modifier(ResponsableEleve responsable)
        {
            if (!ModelState.IsValid)
            {
                return View("Modifier", responsable);
            }
            string role = User.FindFirstValue(ClaimTypes.Role);
            using (ResponsableServices rs = new ResponsableServices())
            {
                rs.ModifierResponsable(responsable);
                return RedirectToAction("TableauDeBord", role);
            }
            
        }

        public IActionResult SupprimerProfil(int responsableId)
        {
            if (responsableId != 0)
            {
                using (ResponsableServices rs = new ResponsableServices())
                {
                    ResponsableEleve responsable = rs.ObtenirTousLesResponsables().Where(r => r.Id == responsableId).FirstOrDefault();
                    if (responsable == null)
                    {
                        return View("Error");
                    }
                    return View("SupprimerProfil", responsable);
                }
            }
            return View("Error");
        }

        public IActionResult Supprimer(int responsableId)
        {
            using (ResponsableServices rs = new ResponsableServices())
            {
                ResponsableEleve responsable = rs.ObtenirTousLesResponsables().Where(r => r.Id == responsableId).FirstOrDefault();
                if (responsable == null)
                {
                    return View("Error");
                }
                foreach (Abonnement abonnement in responsable.Abonnements)
                {
                    if (abonnement.EleveId != null)
                    {
                        using(EleveServices es = new EleveServices())
                        {
                            es.SupprimerEleve((int)abonnement.EleveId);
                        }
                    }
                }
                rs.SupprimerResponsable(responsableId);
                return RedirectToAction("Index");
            }

        }

        public IActionResult TableauDeBord()
        {
            string responsableId = User.FindFirstValue("RoleId");
            ResponsableEleve responsableEleve = null;
            int id;

            using (ResponsableServices rs = new ResponsableServices())
            {
                if (int.TryParse(responsableId, out id))
                {
                    responsableEleve = rs.TrouverUnResponsable(id);
                }
            }

            using (AbonnementServices abs = new AbonnementServices())
            {
                responsableEleve.Abonnements = abs.TrouverAbonnements(id);
            }

            List<Eleve> eleves = new List<Eleve>();
            foreach (Abonnement abonnement in responsableEleve.Abonnements)
            {
                if (abonnement.EleveId != null)
                {
                    Eleve eleve = new Eleve();
                    using (EleveServices es = new EleveServices())
                    {
                        eleve = es.TrouverUnEleve((int)abonnement.EleveId);
                    }

                    using (PrestationServices ps = new PrestationServices())
                    {
                        eleve.Prestations = ps.TouverLesPrestationsDUnEleve(eleve.Id);
                    }

                    eleves.Add(eleve);
                }
                
            }

            TableauBordResponsableViewModel tbrvm = new TableauBordResponsableViewModel() { ResponsableEleve = responsableEleve, Eleves = eleves};

            return View("TableauDeBord", tbrvm);

        }

    }
}
