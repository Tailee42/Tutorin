using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IEleveServices : IDisposable
    {
        void DeleteCreateDatabase();
        int CreerEleve(DateTime dateNaissance, TypeNiveau niveau);
        void ModifierElve(int id, DateTime dateNaissance, TypeNiveau niveau);
        void SupprimerEleve(int id);
        List<Eleve> ObtientTousLesEleves();
    }
}
