using Microsoft.AspNetCore.Mvc;
using Tutorin.Services;
using System.Collections.Generic;
using Tutorin.Models;
using System.Linq;
using Tutorin.ViewModels;
using System.Security.Claims;
using System.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Tutorin.Controllers
{
    public class ResponsableEleveController : Controller
    {
        [Authorize (Roles = "Gestionnaire")]
        public IActionResult Index()
        {
            ResponsableEleveViewModel revm = new ResponsableEleveViewModel();

            using (ResponsableServices rs = new ResponsableServices())
            {
                revm.ListeResponsablesEleves = rs.ObtenirTousLesResponsables();
            }
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
            }

            return RedirectToAction("Index", "Login");
        }

        [Authorize (Roles = "Gestionnaire, ResponsableEleve")]
        [HttpGet]
        public IActionResult Modifier(int responsableId)
        {
            if (responsableId != 0)
            {
                ResponsableEleve responsable = null;
                ResponsableEleveViewModel revm = new ResponsableEleveViewModel();
                using (ResponsableServices rs = new ResponsableServices())
                {
                    responsable = rs.ObtenirTousLesResponsables().Where(r => r.Id == responsableId).FirstOrDefault();
                    revm.ResponsableEleve = responsable;
                }

                if (responsable == null)
                {
                    return View("Error");
                }

                return View("Modifier", revm);
            }
            return View("Error");
        }

        [Authorize(Roles = "Gestionnaire, ResponsableEleve")]
        [HttpPost]
        public IActionResult Modifier(ResponsableEleveViewModel revm)
        {
            //le model state devient false depuis l'ajout de la méthode modifier un mot de passe
            //if (!ModelState.IsValid)
            //{
            //    return View("Modifier", "responsable");
            //}

            string role = User.FindFirstValue(ClaimTypes.Role);
            using (ResponsableServices rs = new ResponsableServices())
            {
                rs.ModifierResponsable(revm.ResponsableEleve.Id, revm.ResponsableEleve.Utilisateur.Nom, revm.ResponsableEleve.Utilisateur.Prenom, revm.ResponsableEleve.Utilisateur.Identifiant, revm.ResponsableEleve.Mail, revm.ResponsableEleve.Abonnements);
            }

            return RedirectToAction("TableauDeBord", role);

        }

        [Authorize(Roles = "ResponsableEleve")]
        [HttpPost]
        public IActionResult ModifierMotdePasse(NewPassword newPassword)
        {
            string responsableId = User.FindFirstValue("RoleId");
            ResponsableEleve responsable = null;
            int id;
            ResponsableEleveViewModel revm = new ResponsableEleveViewModel();

            using (ResponsableServices rs = new ResponsableServices())
            {
                if (int.TryParse(responsableId, out id))
                {
                    responsable = rs.TrouverUnResponsable(id);
                    revm.ResponsableEleve = responsable;
                    rs.ModifierMotdePasse(responsable, newPassword.OldPassword, newPassword.NouveauPassword, newPassword.ConfirmPassword);
                }

                return RedirectToAction("TableauDeBord", "responsableEleve");
            }
        }

        [Authorize(Roles = "ResponsableEleve")]
        public IActionResult SupprimerProfil(int responsableId)
        {
            if (responsableId != 0)
            {
                ResponsableEleve responsable = null;

                using (ResponsableServices rs = new ResponsableServices())
                {
                    responsable = rs.TrouverUnResponsable(responsableId);
                }

                if (responsable == null)
                {
                    return View("Error");
                }

                return View("SupprimerProfil", responsable);
            }
            return View("Error");
        }

        [Authorize(Roles = "ResponsableEleve, Gestionnaire")]
        public IActionResult Supprimer(int responsableId)
        {
            ResponsableEleve responsable;
            string role = User.FindFirstValue(ClaimTypes.Role);

            using (ResponsableServices rs = new ResponsableServices())
            {
                responsable = rs.TrouverUnResponsable(responsableId);
                if (responsable == null)
                {
                    return View("Error");
                }

                using (AbonnementServices abos = new AbonnementServices())
                {
                    foreach (Abonnement abonnement in abos.TrouverAbonnements(responsableId))
                    {
                        abos.FinAbonnement(abonnement.Id);
                        if (abonnement.EleveId != null)
                        {
                            using (EleveServices es = new EleveServices())
                            {
                                es.SupprimerEleve((int)abonnement.EleveId);
                            }
                        }
                    }                    
                }

                rs.SupprimerResponsable(responsableId);
                if (role == "ResponsableEleve")
                {
                    HttpContext.SignOutAsync();
                    return RedirectToAction("Index", "Home");
                } else
                {
                    ResponsableEleveViewModel revm = new ResponsableEleveViewModel()
                    {
                        ListeResponsablesEleves = rs.ObtenirTousLesResponsables()
                    };
                    return View("ListeResponsablesEleves", revm);
                }                          
            }
        }

        [Authorize (Roles = "ResponsableEleve")]
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

            List<Eleve> elevesActifs = new List<Eleve>();
            List<Eleve> elevesFins = new List<Eleve>();
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

                    if (abonnement.DateFin == DateTime.MinValue)
                    {
                        elevesActifs.Add(eleve);
                    } else
                    {
                        elevesFins.Add(eleve);
                    }

                }
                
            }

            TableauBordResponsableViewModel tbrvm = new TableauBordResponsableViewModel() { 
                ResponsableEleve = responsableEleve, 
                ElevesActifs = elevesActifs, 
                ElevesFinAbonnement = elevesFins
            };

            return View("TableauDeBord", tbrvm);

        }

    }
}
