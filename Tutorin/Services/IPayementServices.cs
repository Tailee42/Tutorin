using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IPayementServices : IDisposable
    {
        int CreerPayement(string nomDuTitulairee, string numeroCarte, string dateExpiration, string cvc, float montant);
        void SupprimerPayement(int id);
        List<Payement> ObtientTousLesPayements();
    }
}
