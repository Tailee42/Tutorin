using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using Tutorin.Models;

namespace Tutorin.Services
{
    public class PayementServices : IPayementServices
    {
        private BddContext _bddContext;

        public PayementServices()
        {
            _bddContext = new BddContext();
        }

        public int CreerPayement(string nomDuTitulairee, string numeroCarte, string dateExpiration, string cvc, float montant)
        {
            Payement payement = new Payement() { NomTitulaireCarte = nomDuTitulairee, NumeroCarte = numeroCarte, 
                DateExpiration = dateExpiration, CVC = cvc, MontantTTC = montant };
                _bddContext.Payements.Add(payement);
                _bddContext.SaveChanges();
            return payement.Id;
        }

        public int CreerPayement(Payement payement)
        {
            _bddContext.Payements.Add(payement);
            _bddContext.SaveChanges();
            return payement.Id;
        }

        public void Dispose()
        {
           _bddContext.Dispose();
        }

        public List<Payement> ObtientTousLesPayements()
        {
            List<Payement> list = _bddContext.Payements.ToList();
            foreach (Payement payement in list)
            {
                payement.ResponsableEleve = _bddContext.ResponsablesEleves.Find(payement.ResponsableEleveId);
            }

            return list;
        }

        public void SupprimerPayement(int id)
        {
            Payement payement = _bddContext.Payements.Find(id);
            _bddContext.Payements.Remove(payement);
            _bddContext.SaveChanges();
        }
    }
}
