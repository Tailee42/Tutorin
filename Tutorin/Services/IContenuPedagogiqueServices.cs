using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IContenuPedagogiqueServices : IDisposable
    {
        int CreerContenuPedagogique(TypeMatiere matiere, TypeNiveau niveau, string titre, string contenu, int enseignantId);
        int CreerContenuPedagogique(ContenuPedagogique cours);
        void ModifierContenuPedagogique(ContenuPedagogique cours);
        List<ContenuPedagogique> ObtenirTousLesContenusPedagogiques();
        void SupprimerContenuPedagogique(int id);
        List<ContenuPedagogique> RechercherCoursParNiveau(TypeNiveau niveau);
    }
}
