﻿using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public class ResponsableServices : IResponsableServices
    {
        private BddContext _bddContext;

        public ResponsableServices()
        {
            _bddContext = new BddContext();
        }

        public int CreerResponsable(int utilisateurId, List<Abonnement> abonnements)
        {
            ResponsableEleve responsable = new ResponsableEleve() {UtilisateurId = utilisateurId, Abonnements = abonnements};
            _bddContext.ResponsablesEleves.Add(responsable);
            _bddContext.SaveChanges();
            return responsable.Id;
        }



        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
