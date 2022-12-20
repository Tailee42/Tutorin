using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;

namespace Tutorin.Controllers
{
    public class PayementController : Controller
    {
        [Authorize (Roles = "ResponsableEleve")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "ResponsableEleve")]
        public IActionResult PayerAbonnement(int typeAbonnement)
        {
            int roleId;
            ResponsableEleve responsableEleve = null;
            if (int.TryParse(User.FindFirstValue("RoleId"), out roleId))
            {
                using (ResponsableServices rs = new ResponsableServices())
                {
                    responsableEleve = rs.TrouverUnResponsable(roleId);
                }

            }

            Payement payement = new Payement() { NomTitulaireCarte = responsableEleve.Utilisateur.Nom, NumeroCarte = "1234123412341234", DateExpiration = "03/24", CVC = "789", ResponsableEleve = responsableEleve, ResponsableEleveId = roleId, MontantTTC = TypeAbonnementExtensions.PrixTTCAbonnement((TypeAbonnement)typeAbonnement)};
            PayementViewModel pvm = new PayementViewModel() { TypeAbonnement = (TypeAbonnement)typeAbonnement, ResponsableEleve = responsableEleve, Payement = payement };

            return View("PayementAbonnement", pvm);
        }

        [Authorize, HttpPost]
        public IActionResult PayerAbonnement(Payement payement, PayementViewModel pvm)
        {
            if (!ModelState.IsValid)
            {
                return View("PayementAbonnement", pvm);
            }

            if (int.TryParse(User.FindFirstValue("RoleId"), out int roleId))
            {
                payement.ResponsableEleveId = roleId;
            }

            int payementId;
            using (PayementServices ps = new PayementServices())
            {
                payement.MontantTTC = TypeAbonnementExtensions.PrixTTCAbonnement(pvm.TypeAbonnement);
                payementId = ps.CreerPayement(payement);
            }

            using (AbonnementServices abs = new AbonnementServices())
            {
                abs.CreerAbonnement(payementId, pvm.TypeAbonnement, roleId);
            }

            return RedirectToAction("TableauDeBord", User.FindFirstValue(ClaimTypes.Role));
        }

        [Authorize]
        public IActionResult PayerPrestation(PrestationViewModel pvm)
        {
            int roleId;
            ResponsableEleve responsableEleve = null;
            if (int.TryParse(User.FindFirstValue("RoleId"), out roleId))
            {
                using (ResponsableServices rs = new ResponsableServices())
                {
                    responsableEleve = rs.TrouverUnResponsable(roleId);
                }
            }

            using (PrestationServices ps = new PrestationServices())
            {
                pvm.Prestation = ps.TrouverUnePrestation(pvm.PrestationId);
            }

            Payement payement = new Payement() { NomTitulaireCarte = responsableEleve.Utilisateur.Nom, NumeroCarte = "1234123412341234", DateExpiration = "03/24", CVC = "789", ResponsableEleve = responsableEleve, ResponsableEleveId = roleId };
            payement.MontantTTC = pvm.Prestation.Prix * pvm.ElevesId.Count;

            pvm.Payement = payement;
                
            return View("PayementPrestation", pvm);
        }

        [Authorize, HttpPost]
        public IActionResult PayerPrestation(Payement payement, PrestationViewModel pvm)
        {
            if (!ModelState.IsValid)
            {
                return View("PayementPrestation", pvm);
            }

            if (int.TryParse(User.FindFirstValue("RoleId"), out int roleId))
            {
                payement.ResponsableEleveId = roleId;
            }

            using (PrestationServices ps = new PrestationServices())
            {
                pvm.Prestation = ps.TrouverUnePrestation(pvm.PrestationId);
            }

            int payementId;
            using (PayementServices ps = new PayementServices())
            {
                payement.MontantTTC = pvm.Prestation.Prix * pvm.ElevesId.Count;

                payementId = ps.CreerPayement(payement);
            }


            using (PrestationServices ps = new PrestationServices())
            {
                ps.AjouterUnPayement(pvm.PrestationId, payementId);

                foreach (int id in pvm.ElevesId)
                    ps.InscrireEleveAPrestation(id, pvm.PrestationId);
            }

            return RedirectToAction("TableauDeBord", User.FindFirstValue(ClaimTypes.Role));

        }
    }
}
