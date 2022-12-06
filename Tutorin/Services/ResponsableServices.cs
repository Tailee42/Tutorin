using System.Collections.Generic;
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

        public int CreerResponsable(Utilisateur utilisateur, List<Abonnement> abonnements)
        {
            ResponsableEleve responsable = new ResponsableEleve() {UtilisateurId = utilisateur.Id, Abonnements = abonnements};
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
