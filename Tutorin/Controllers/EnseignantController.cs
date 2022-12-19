using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;
using Microsoft.AspNetCore.Hosting;

namespace Tutorin.Controllers
{
    public class EnseignantController : Controller
    {

        private IWebHostEnvironment _webEnv;

        public EnseignantController(IWebHostEnvironment environment)
        {
            _webEnv = environment;
        }

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

            if (enseignant.Image != null )
            {
                if (enseignant.Image.Length != 0) {
                string uploads = Path.Combine(_webEnv.WebRootPath, "images");
                string filePath = Path.Combine(uploads, enseignant.Image.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))

                {
                    enseignant.Image.CopyTo(fileStream);
                }
                }

                enseignant.ImagePath = enseignant.Image.FileName; 
            }
                   
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
                using (EnseignantServices ens = new EnseignantServices())
                {
                    enseignant = ens.ObtientTousLesEnseignants().Where(r => r.Id == enseignantId).FirstOrDefault();
                }

                if (enseignant == null)
                {
                    return View("Error");
                }
                return View("Modifier", enseignant);
            }
            return View("Error");
        }

        [Authorize(Roles = "Gestionnaire, Enseignant")]
        [HttpPost]
        public IActionResult Modifier(Enseignant enseignant)
        {
            if (!ModelState.IsValid)
            {
                return View("Modifier", enseignant);
            }
            
            string role = User.FindFirstValue(ClaimTypes.Role);

            using (EnseignantServices ens = new EnseignantServices())
            {
                ens.ModifierEnseignant(enseignant);
            }

            return RedirectToAction("TableauDeBord", role);
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
