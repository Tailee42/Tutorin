using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class PrestationViewModel
    {
        public List<Prestation> ListePrestations { get; set; }
        public ResponsableEleve ResponsableEleve { get; set; }
        public Prestation Prestation { get; set; }
    }
}
