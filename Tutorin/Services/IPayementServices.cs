using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IPayementServices : IDisposable
    {
        int CreerPayement(string nomDuTitulairee, int numeroCarte, int dateExpiration, int cvc, float montant);
        void SupprimerPayement(int id);
        List<Payement> ObtientTousLesPayements();
    }
}
