using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Tutorin.Models;
using Tutorin.Services;
using Tutorin.ViewModels;

namespace Tutorin.Controllers
{
    public class PrestationController : Controller
    {
        public IActionResult Index()
        {
            List<Prestation> listePrestations = new List<Prestation>();
            PrestationViewModel pvm;

            using (PrestationServices ps = new PrestationServices())
            {
                pvm = new PrestationViewModel()
                {
                    ListePrestations = ps.ObtientTousLesPrestations()
                };
            };

            return View("ListePrestations", pvm);
        }
    }
}
