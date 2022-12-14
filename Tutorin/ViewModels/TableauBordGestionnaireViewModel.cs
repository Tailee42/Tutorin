using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class TableauBordGestionnaireViewModel
    {
        public Gestionnaire Gestionnaire { get; set; }
        public int NbCoursAValider { get; set; }
        public int NbCoursAModifier { get; set; }
        public int NbCoursEnLigne { get; set; }
        public int NbCoursTotal { get; set; }

    }
}
