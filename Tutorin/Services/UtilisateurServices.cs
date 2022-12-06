using System.Collections.Generic;
using System.Linq;
using Tutorin.Models;

namespace Tutorin.Services
{
    public class UtilisateurServices : IUtilisateurServices
    {
        private BddContext _bddContext;

        public UtilisateurServices()
        {
            _bddContext = new BddContext();
        }

        public int CreerUtilisateur(string nom, string prenom, string identifiant, string motDePasse)
        {
            Utilisateur utilisateur = new Utilisateur() { Nom = nom, Prenom = prenom, Identifiant = identifiant, MotDePasse = motDePasse};
            _bddContext.Utilisateurs.Add(utilisateur);
            _bddContext.SaveChanges();
            return utilisateur.Id;
        }

        public void ModifierUtilisateur(int id, string nom, string prenom, string identifiant, string motDePasse)
        {
            Utilisateur utilisateur = _bddContext.Utilisateurs.Find(id);
            if (utilisateur != null)
            {
                utilisateur.Nom = nom;
                utilisateur.Prenom = prenom;
                utilisateur.Identifiant = identifiant;
                utilisateur.MotDePasse = motDePasse;
                _bddContext.SaveChanges();
            }
        }

        public void SupprimerUtilisateur(int id)
        {
            Utilisateur utilisateur = _bddContext.Utilisateurs.Find(id);
            _bddContext.Utilisateurs.Remove(utilisateur);
            _bddContext.SaveChanges();
        }

        public List<Utilisateur> ObtientTousLesUtilisateurs()
        {
            return _bddContext.Utilisateurs.ToList();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
