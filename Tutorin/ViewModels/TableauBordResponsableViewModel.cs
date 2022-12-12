using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class TableauBordResponsableViewModel
    {
        public ResponsableEleve ResponsableEleve { get; set; }
        public List<Abonnement> Abonnements { get; set; }

    }
}
