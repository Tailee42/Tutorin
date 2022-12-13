using Microsoft.AspNetCore.Mvc;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class PayementViewModel
    {
        public TypeAbonnement TypeAbonnement { get; set; }
        public Payement Payement { get; set; }
        public ResponsableEleve ResponsableEleve { get; set; }
    }
}
