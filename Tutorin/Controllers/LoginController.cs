using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
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
                            role = "Responsable";
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

                    var userClaims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, utilisateur.Id.ToString()),
                        new Claim(ClaimTypes.Actor, role.ToString()),
                        new Claim("RoleId", roleId.ToString())
                    };

                    var ClaimIdentity = new ClaimsIdentity(userClaims, "User Identity");
                    var userPrincipal = new ClaimsPrincipal(new[] { ClaimIdentity });
                    HttpContext.SignInAsync(userPrincipal);
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    //Premet d'obtenir la page d'accueil correspondant au rôle
                    switch (role)
                    {
                        case "Enseignant":
                            return RedirectToAction("Index", "Enseignant");
                        case "Responsable":
                            return RedirectToAction("Index", "ResponsableEleve");
                        case "Eleve":
                            return RedirectToAction("Index", "Eleve");
                        default:
                            return Redirect("/");
                    }

                }
                ModelState.AddModelError("Utilisateur.Identifiant", "Identifiant et/ou mot de passe incorrect(s)");
            }
            return View(viewModel);
        }

        public IActionResult CreerCompte()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreerCompte(Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                int id = us.AjouterUtilisateur(utilisateur.Nom, utilisateur.Prenom, utilisateur.Identifiant, utilisateur.MotDePasse);
                var userClaims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, id.ToString()),
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
