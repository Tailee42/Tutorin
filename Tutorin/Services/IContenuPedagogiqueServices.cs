using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IContenuPedagogiqueServices : IDisposable
    {
        int CreerContenuPedagogique(TypeMatiere matiere, TypeNiveau niveau, string titre, string contenu, int enseignantId);
        int CreerContenuPedagogique(ContenuPedagogique cours);
        List<ContenuPedagogique> ObtientTousLesContenusPedagogiques();
        void SupprimerContenuPedagogique(int id);
    }
}
