using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class ContenuPedagogiqueViewModel
    {
        public List<ContenuPedagogique> ListeContenusPedagogiques { get; set; }
        public TypeNiveau Niveau { get; set; }
        public TypeMatiere Matiere { get; set; }

    }
}
