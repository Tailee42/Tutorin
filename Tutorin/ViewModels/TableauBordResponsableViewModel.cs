using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class TableauBordResponsableViewModel
    {
        public ResponsableEleve ResponsableEleve { get; set; }
        public List<Eleve> Eleves { get; set; }

        public List<Prestation> Prestations { get; set; }
        
    }
}
