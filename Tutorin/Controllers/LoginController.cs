using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;

namespace Tutorin.Controllers
{
    public class LoginController : Controller
    {
        private UtilisateurServices us;
        public LoginController()
        {
            us = new UtilisateurServices();
        }
        public IActionResult Index()
        {
            UtilisateurViewModel viewModel = new UtilisateurViewModel { Authentifie = HttpContext.User.Identity.IsAuthenticated };
            if (viewModel.Authentifie)
            {
                viewModel.Utilisateur = us.ObtenirUtilisateur(HttpContext.User.Identity.Name);

                return View(viewModel);
                
            }
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Index(UtilisateurViewModel viewModel, string returnUrl)
        {
            if (viewModel.Utilisateur.Identifiant != null && viewModel.Utilisateur.MotDePasse != null)
            {
                Utilisateur utilisateur = us.Authentifier(viewModel.Utilisateur.Identifiant, viewModel.Utilisateur.MotDePasse);
                if (utilisateur != null)
                {
                    int roleId = 0;
                    string role = "";
                    // Cherche l'utilisateur dans les tables enseignants, responsables, élèves
                    using (EnseignantServices ens = new EnseignantServices())
                    {
                        Enseignant enseignant = ens.ObtientTousLesEnseignants().Where(r => r.UtilisateurId == utilisateur.Id).FirstOrDefault();
                        if (enseignant != null)
                        {
                            role = "Enseignant";
                            roleId = enseignant.Id;
                        }
                    }

                    using (ResponsableServices rs = new ResponsableServices())
                    {
                        ResponsableEleve responsable = rs.ObtenirTousLesResponsables().Where(r => r.UtilisateurId == utilisateur.Id).FirstOrDefault();
                        if (responsable != null)
                        {
                            role = "ResponsableEleve";
                            roleId = responsable.Id;
                        }
                    }

                    using (EleveServices els = new EleveServices())
                    {
                        Eleve eleve = els.ObtientTousLesEleves().Where(r => r.UtilisateurId == utilisateur.Id).FirstOrDefault();
                        if (eleve != null)
                        {
                            role = "Eleve";
                            roleId = eleve.Id;
                        }
                    }

                    using (GestionnaireServices gs = new GestionnaireServices())
                    {
                        Gestionnaire gestionnaire = gs.ObtientTousLesGestionnaires().Where(r => r.UtilisateurId == utilisateur.Id).FirstOrDefault();
                        if (gestionnaire != null)
                        {
                            role = "Gestionnaire";
                            roleId = gestionnaire.Id;
                        }
                    }

                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, utilisateur.Id.ToString()),
                        new Claim(ClaimTypes.Role, role.ToString()),
                        new Claim("RoleId", roleId.ToString())
                    };

                    User.FindFirstValue(ClaimTypes.Role);

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                    HttpContext.SignInAsync(userPrincipal);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    //Premet d'obtenir la page d'accueil correspondant au rôle
                    switch (role)
                    {
                        case "Enseignant":
                            return RedirectToAction("VoirPrestationAAffecter", "Prestation");
                        case "ResponsableEleve":
                            return RedirectToAction("TableauDeBord", "ResponsableEleve");
                        case "Eleve":
                            return RedirectToAction("TableauDeBord", "Eleve");
                        case "Gestionnaire":
                            return RedirectToAction("TableauDeBord", "Gestionnaire");
                        default:
                            return Redirect("/");
                    }

                }
                ModelState.AddModelError("Utilisateur.Identifiant", "Identifiant et/ou mot de passe incorrect(s)");
            }
            return View(viewModel);
        }

        public IActionResult CreerCompteUtilisateur()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreerCompteUtilisateur(Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                int id = us.AjouterUtilisateur(utilisateur.Nom, utilisateur.Prenom, utilisateur.Identifiant, utilisateur.MotDePasse);
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, utilisateur.Id.ToString())
                };
                var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                HttpContext.SignInAsync(userPrincipal);
                return Redirect("/");
            }
            return View(utilisateur);
        }
        public ActionResult Deconnexion()
        {
            HttpContext.SignOutAsync();
            return Redirect("/");
        }
    }
}
