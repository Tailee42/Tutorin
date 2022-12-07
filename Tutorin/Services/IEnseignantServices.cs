using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IEnseignantServices : IDisposable
    {
       
        int CreerEnseignant(string matiere, TypeNiveau niveau, int utilisateurID);

        //void ModifierEnseignant(int id, string nom, string prenom, string identifiant, string motDePasse);
        //void SupprimerEnseignant(int id);
        List<Enseignant> ObtientTousLesEnseignants();
    }
}
