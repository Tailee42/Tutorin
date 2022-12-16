using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class TableauBordResponsableViewModel
    {
        public ResponsableEleve ResponsableEleve { get; set; }
        public List<Eleve> Eleves { get; set; }
        public List<Eleve> ElevesActifs { get; set; }
        public List<Eleve> ElevesFinAbonnement { get; set; }

        
    }
}
