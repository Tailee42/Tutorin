using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IResponsableServices : IDisposable
    {
        int CreerResponsable(int utilisateurId, List<Abonnement> abonnements);
        void ModifierResponsable(int id, int utilisateurId, List<Abonnement> abonnements);
    }
}
