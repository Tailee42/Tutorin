using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class EnseignantViewModel
    {
        public Enseignant Enseignant { get; set; }
        public List<Enseignant> ListeEnseignants { get; set; }
        public List<Prestation> Prestations { get; set; }
        public List<ContenuPedagogique> ContenuPedagogiques { get; set; }
    }
}
