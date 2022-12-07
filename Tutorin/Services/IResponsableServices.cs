using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IResponsableServices : IDisposable
    {
        int CreerResponsable(string mail, int utilisateurId, List<Abonnement> abonnements);
        void ModifierResponsable(int id, string mail, int utilisateurId, List<Abonnement> abonnements);
        void SupprimerResponsable(int id, int utilisateurId);
        List<ResponsableEleve> ObtenirTousLesResponsables();
    }
}
