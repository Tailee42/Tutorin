using System.Collections.Generic;
using System.Linq;
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

        public int CreerResponsable(string mail, int utilisateurId, List<Abonnement> abonnements)
        {
            ResponsableEleve responsable = new ResponsableEleve() {UtilisateurId = utilisateurId, Abonnements = abonnements};
            _bddContext.ResponsablesEleves.Add(responsable);
            _bddContext.SaveChanges();
            return responsable.Id;
        }

        public void ModifierResponsable(int id, string nom, string prenom, string identifiant, string motDePasse, string mail, int utilisateurId, List<Abonnement> abonnements)
        {
            ResponsableEleve responsable = _bddContext.ResponsablesEleves.Find(id);
            responsable.Utilisateur = _bddContext.Utilisateurs.Find(utilisateurId);

            if (responsable != null)
            {
                responsable.Utilisateur.Nom = nom;
                responsable.Utilisateur.Prenom = prenom;
                responsable.Utilisateur.Identifiant = identifiant;
                responsable.Utilisateur.MotDePasse = motDePasse;
                responsable.Mail = mail;
                responsable.Abonnements = abonnements;
            }
        }

        public void SupprimerResponsable(int id, int utilisateurId)
        {
            ResponsableEleve responsable = _bddContext.ResponsablesEleves.Find(id);
            responsable.Utilisateur = _bddContext.Utilisateurs.Find(utilisateurId);
            _bddContext.ResponsablesEleves.Remove(responsable);
            _bddContext.Utilisateurs.Remove(responsable.Utilisateur);
            _bddContext.SaveChanges();
        }

        public List<ResponsableEleve> ObtenirTousLesResponsables()
        {
            List<ResponsableEleve> listResponsables = new List<ResponsableEleve>();

            foreach (var responsable in listResponsables)
            {
                responsable.Utilisateur = _bddContext.Utilisateurs.Find(responsable.UtilisateurId);
            }
            return listResponsables;
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
