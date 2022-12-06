using System;
using System.Collections.Generic;
using System.Linq;
using Tutorin.Models;

namespace Tutorin.Services
{
    public class EleveServices : IEleveServices
    {
        private BddContext _bddContext;

        public EleveServices()
        {
            _bddContext = new BddContext();
        }

        public int CreerEleve(DateTime dateNaissance, TypeNiveau niveau, int utilisateurID)
        {
            Eleve eleve = new Eleve() { DateNaissance = dateNaissance, Niveau = niveau, UtilisateurID = utilisateurID};
            _bddContext.Eleves.Add(eleve);
            _bddContext.SaveChanges();

            return eleve.Id;
        }

        public void DeleteCreateDatabase()
        {
            _bddContext.Database.EnsureDeleted();
            _bddContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public void ModifierElve(int id, DateTime dateNaissance, TypeNiveau niveau)
        {
            Eleve eleve = _bddContext.Eleves.Find(id);
            if (eleve != null)
            {
                eleve.DateNaissance = dateNaissance;
                eleve.Niveau = niveau;
               _bddContext.SaveChanges();
            }
        }

        public List<Eleve> ObtientTousLesEleves()
        {
            return _bddContext.Eleves.ToList();
        }

        public void SupprimerEleve(int id)
        {
            Eleve eleve = _bddContext.Eleves.Find(id);
            _bddContext.Eleves.Remove(eleve);
            _bddContext.SaveChanges();
        }
    }
}
