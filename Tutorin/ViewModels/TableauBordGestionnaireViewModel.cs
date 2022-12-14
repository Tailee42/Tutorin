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

        public int NbPrestationAAffecter { get; set; }
        public int NbPrestationEnseignantsInscrits { get; set; }
        public int NbPrestationPayeeParResponsable { get; set; }
        public int NbPrestationRealisees { get; set; }
        public int NbPrestationEnseigantPayes { get; set; }
        public int NbPrestationFacturees { get; set; }
        public int NbPrestationAnnulees { get; set; }
        public int NbPrestationTotal { get; set; }

        public int NbResponsableEleve { get; set; }
        public int NbEnseignant { get; set; }
        public int NbEleve { get; set; }
        public int NbGestionnaire { get; set; }
        public int NbUtilisateurTotal { get; set; }
    }
}
