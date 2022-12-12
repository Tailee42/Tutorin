using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;

namespace Tutorin.Controllers
{
    public class PayementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
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

            Payement payement = new Payement() { NomTitulaireCarte = responsableEleve.Utilisateur.Nom, NumeroCarte = "1234123412341234", DateExpiration = "03/24", CVC = "789", ResponsableEleve = responsableEleve, ResponsableEleveId = roleId, MontantTTC = TypeAbonnementExtensions.PrixTTCAbonnement(typeAbonnement)};
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

            int roleId;
            if (int.TryParse(User.FindFirstValue("RoleId"), out roleId))
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
                abs.CreerAbonnement(payementId, pvm.TypeAbonnement, roleId );
            }

            return RedirectToAction("TableauDeBord", User.FindFirstValue(ClaimTypes.Role));

        }
    }
}
