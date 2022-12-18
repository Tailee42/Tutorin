using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IEnseignantServices : IDisposable
    {
       
        int CreerEnseignant(TypeMatiere matiere, TypeNiveau niveau, int utilisateurID);

        void ModifierEnseignant(int id, string nom, string prenom, string identifiant, TypeMatiere matiere, TypeNiveau niveau);
        void SupprimerEnseignant(int id);
        List<Enseignant> ObtientTousLesEnseignants();
        int CompterEnseignant();
    }
}
