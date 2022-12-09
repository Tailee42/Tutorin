using Microsoft.AspNetCore.Mvc;
using Tutorin.Services;
using System.Collections.Generic;
using Tutorin.Models;
using System.Linq;
using Tutorin.ViewModels;

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
                return RedirectToAction("Index");
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
            
            using (ResponsableServices rs = new ResponsableServices())
            {
                rs.ModifierResponsable(responsable);
                return RedirectToAction("Index");
            }
            
        }

        public IActionResult Supprimer(int responsableId)
        {
            using (ResponsableServices rs = new ResponsableServices())
            {
                rs.SupprimerResponsable(responsableId);
                return RedirectToAction("Index");
            }
        }
    }
}
