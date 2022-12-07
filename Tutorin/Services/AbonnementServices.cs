using System;
using System.Collections.Generic;
using System.Linq;
using Tutorin.Models;

namespace Tutorin.Services
{
    public class AbonnementServices : IAbonnementServices
    {
        private BddContext _bddContext;

        public AbonnementServices()
        {
            _bddContext = new BddContext();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public int CreerAbonnement(TypeAbonnement type, DateTime dateDebut, DateTime dateFin, float prix, int responsableEleveId, int eleveId)
        {
            Abonnement abonnement = new Abonnement() { Type = type, DateDebut = dateDebut, DateFin = dateFin, Prix = prix, ResponsableEleveId = responsableEleveId, EleveId = eleveId};
            _bddContext.Abonnements.Add(abonnement);
            _bddContext.SaveChanges();
            return abonnement.Id;
        }

        public void ModifierAbonnement(int id, TypeAbonnement type, DateTime dateDebut, DateTime dateFin, float prix, int responsableEleveId, int eleveId)
        {
            Abonnement abonnement = _bddContext.Abonnements.Find(id);

            if (abonnement != null)
            {
                abonnement.Type = type;
                abonnement.DateDebut = dateDebut;
                abonnement.DateFin = dateFin;
                abonnement.Prix = prix;
            }
        }

        public void SupprimerAbonnement(int id)
        {
            Abonnement abonnement = _bddContext.Abonnements.Find(id);
            _bddContext.Abonnements.Remove(abonnement);
            _bddContext.SaveChanges();
        }

        public List<Abonnement> ObtenirTousLesAbonnements()
        {
            return _bddContext.Abonnements.ToList();
        }

    }
}
