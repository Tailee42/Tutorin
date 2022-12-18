using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IGestionnaireServices : IDisposable
    {
        int CreerGestionnaire(string posteOccupe, int utilisateurId);
        void ModifierGestionnaire(int id, string nom, string prenom, string identifiant, string posteOccupe);
        void SupprimerGestionnaire(int id);
        List<Gestionnaire> ObtientTousLesGestionnaires();
        int CompterGestionnaire();
        void ModifierMotdePasse(Gestionnaire gestionnaire, string ancienMdp, string newMdp, string confirmMdp);
    }
}
