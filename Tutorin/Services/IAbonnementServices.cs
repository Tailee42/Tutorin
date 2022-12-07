using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IAbonnementServices : IDisposable
    {
        int CreerAbonnement(TypeAbonnement type, DateTime dateDebut, DateTime dateFin, float prix, int responsableEleveId, int eleveId);
        void ModifierAbonnement(int id, TypeAbonnement type, DateTime dateDebut, DateTime dateFin, float prix, int responsableEleveId, int eleveId);
        void SupprimerAbonnement(int id);
        List<Abonnement> ObtenirTousLesAbonnements();
    }
}
