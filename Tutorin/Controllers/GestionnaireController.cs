using Microsoft.AspNetCore.Mvc;
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
            TableauBordGestionnaireViewModel tbevm = new TableauBordGestionnaireViewModel();
            using (ContenuPedagogiqueServices cps = new ContenuPedagogiqueServices())
            {
                tbevm.NbCoursAValider = cps.CompterCoursSelonEtat(EtatContenuPedagogique.A_Valider);
                tbevm.NbCoursAModifier = cps.CompterCoursSelonEtat(EtatContenuPedagogique.A_Modifier);
                tbevm.NbCoursEnLigne = cps.CompterCoursSelonEtat(EtatContenuPedagogique.En_Ligne);
                tbevm.NbCoursTotal = cps.CompterTotalCours();
            };

            using (PrestationServices ps = new PrestationServices())
            {
                tbevm.NbPrestationAAffecter = ps.CompterPrestationSelonEtat(EtatPrestation.A_affecter);
                tbevm.NbPrestationEnseignantsInscrits = ps.CompterPrestationSelonEtat(EtatPrestation.Enseignants_inscrits);
                tbevm.NbPrestationPayeeParResponsable = ps.CompterPrestationSelonEtat(EtatPrestation.Payee_par_responsable_eleve);
                tbevm.NbPrestationRealisees = ps.CompterPrestationSelonEtat(EtatPrestation.Realisée);
                tbevm.NbPrestationEnseigantPayes = ps.CompterPrestationSelonEtat(EtatPrestation.Enseigants_payes);
                tbevm.NbPrestationFacturees = ps.CompterPrestationSelonEtat(EtatPrestation.Facturee);
                tbevm.NbPrestationAnnulees = ps.CompterPrestationSelonEtat(EtatPrestation.Annulee);
                tbevm.NbPrestationTotal = ps.CompterTotalPrestations();
            };

            using (ResponsableServices rs = new ResponsableServices())
            {
                tbevm.NbResponsableEleve = rs.CompterResponsableEleve();
            };

            using (EleveServices es = new EleveServices())
            {
                tbevm.NbEleve = es.CompterEleve();
            };

            using (EnseignantServices ens = new EnseignantServices())
            {
                tbevm.NbEnseignant = ens.CompterEnseignant();
            };

            using (GestionnaireServices gs = new GestionnaireServices())
            {
                tbevm.Gestionnaire = gestionnaire;
                tbevm.NbGestionnaire = gs.CompterGestionnaire();
            };
                    
            using (UtilisateurServices us = new UtilisateurServices())
            {
                tbevm.NbUtilisateurTotal = us.CompterUtilisateur();
                
            };
            return View("TableauDeBord", tbevm);
        }

        public IActionResult GererUtilisateurs()
        {
            //Gestionnaire gestionnaire = 

            //TableauBordGestionnaireViewModel tbevm = new TableauBordGestionnaireViewModel()
            //{
            //    Gestionnaire = gestionnaire,
            //};

            return View("GererUtilisateurs");
        }
    }
}
