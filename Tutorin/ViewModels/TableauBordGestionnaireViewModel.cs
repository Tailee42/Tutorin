using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class TableauBordGestionnaireViewModel
    {
        public Gestionnaire Gestionnaire { get; set; }
        public List<Prestation> PrestationsAAffectees { get; set; }
        public List<ContenuPedagogique> CoursAPublier { get; set; }
    }
}
