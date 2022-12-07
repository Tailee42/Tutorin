using System.Collections.Generic;
using Tutorin.Models;
using System.Linq;


namespace Tutorin.Services
{
    public class EnseignantServices : IEnseignantServices
    {
        private BddContext _bddContext;
        public EnseignantServices()
        {
            _bddContext = new BddContext();
        }

        public int CreerEnseignant ( string matiere, TypeNiveau niveau, int utilisateurID)
        {
            Enseignant enseignant = new Enseignant() { Matiere = matiere, Niveaux = niveau, UtilisateurId = utilisateurID };
            _bddContext.Enseignants.Add(enseignant);
            _bddContext.SaveChanges();
            return enseignant.Id;
        }

        public void ModiferEnseignant(int id, string nom, string prenom, string identifiant, string motDePasse, string matiere, TypeNiveau niveau, int utilisateurID)
        {
            Enseignant enseignant = _bddContext.Enseignants.Find(id);
            enseignant.Utilisateur = _bddContext.Utilisateurs.Find(enseignant.UtilisateurId);

            if (enseignant != null)
            {
                enseignant.Utilisateur.Nom = nom;
                enseignant.Utilisateur.Prenom = prenom;
                enseignant.Utilisateur.Identifiant = identifiant;
                enseignant.Utilisateur.MotDePasse = motDePasse;
                _bddContext.SaveChanges();
            }
        }

        public List<Enseignant> ObtientTousLesEnseignants()
        {
            return _bddContext.Enseignants.ToList();
        }
        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }

}
