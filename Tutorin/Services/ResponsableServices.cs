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

        public void CreerResponsable(ResponsableEleve responsable)
        {
            responsable.Utilisateur.MotDePasse = UtilisateurServices.EncodeMD5(responsable.Utilisateur.MotDePasse);
            _bddContext.ResponsablesEleves.Add(responsable);
            _bddContext.SaveChanges();
        }

        public void ModifierResponsable(int id, string nom, string prenom, string identifiant, string motDePasse, string mail, List<Abonnement> abonnements)
        {
            ResponsableEleve responsable = _bddContext.ResponsablesEleves.Find(id);
            responsable.Utilisateur = _bddContext.Utilisateurs.Find(responsable.Utilisateur.Id);

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

        public void ModifierResponsable(ResponsableEleve responsable)
        {
            responsable.Utilisateur.MotDePasse = UtilisateurServices.EncodeMD5(responsable.Utilisateur.MotDePasse);
            _bddContext.ResponsablesEleves.Update(responsable);
            _bddContext.SaveChanges();
        }

        public void SupprimerResponsable(int id)
        {
            ResponsableEleve responsable = _bddContext.ResponsablesEleves.Find(id);
            responsable.Utilisateur = _bddContext.Utilisateurs.Find(responsable.UtilisateurId) ;
            _bddContext.ResponsablesEleves.Remove(responsable);
            _bddContext.Utilisateurs.Remove(responsable.Utilisateur);
            _bddContext.SaveChanges();
        }

        public List<ResponsableEleve> ObtenirTousLesResponsables()
        {
            List<ResponsableEleve> listResponsables = _bddContext.ResponsablesEleves.ToList();

            foreach (ResponsableEleve responsable in listResponsables)
            {
                responsable.Utilisateur = _bddContext.Utilisateurs.Find(responsable.UtilisateurId);
            }
            return listResponsables;
        }

        public ResponsableEleve TrouverUnResponsable(int id)
        {
            ResponsableEleve responsable = _bddContext.ResponsablesEleves.Find(id);
            responsable.Utilisateur = _bddContext.Utilisateurs.Find(responsable.UtilisateurId);

            return responsable;
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public int CompterResponsableEleve()
        {
            int nbResponsable = _bddContext.ResponsablesEleves.Count();
            return nbResponsable;
        }
    }
}
