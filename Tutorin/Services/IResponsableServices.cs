using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IResponsableServices : IDisposable
    {
        int CreerResponsable(string mail, int utilisateurId, List<Abonnement> abonnements);
        void CreerResponsable(ResponsableEleve responsable);
        void ModifierResponsable(int id, string nom, string prenom, string identifiant, string motDePasse, string mail, List<Abonnement> abonnements);
        void ModifierResponsable(ResponsableEleve responsable);
        void SupprimerResponsable(int id);
        List<ResponsableEleve> ObtenirTousLesResponsables();
    }
}
