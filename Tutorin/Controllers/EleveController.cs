using Microsoft.AspNetCore.Mvc;
using Tutorin.Services;
using Tutorin.ViewModels;
using System.Collections.Generic;
using Tutorin.Models;

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

        public IActionResult Ajouter()
        {
            return View("Ajouter");
        }

        [HttpPost]
        public IActionResult Ajouter(Eleve eleve)
        {
            if (!ModelState.IsValid)
            {
                return View("AjouterUtilisateur", eleve);
            }

            using (EleveServices el = new EleveServices())
            {
                el.CreerEleve(eleve);
                return RedirectToAction("Index");
            }
        }
    }
}
