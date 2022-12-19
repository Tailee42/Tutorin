using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IEleveServices : IDisposable
    {
        int CreerEleve(DateTime dateNaissance, TypeNiveau niveau, int utilisateurID);
        void ModifierEleve(int id, string nom, string prenom, string identifiant, DateTime dateNaissance, TypeNiveau niveau);
        void SupprimerEleve(int id);
        List<Eleve> ObtientTousLesEleves();
        int CompterEleve();
    }
}
