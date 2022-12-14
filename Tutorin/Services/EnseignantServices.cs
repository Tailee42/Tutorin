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

        public int CreerEnseignant ( TypeMatiere matiere, TypeNiveau niveau, int utilisateurID)
        {
            Enseignant enseignant = new Enseignant() { Matiere = matiere, Niveaux = niveau, UtilisateurId = utilisateurID };
            _bddContext.Enseignants.Add(enseignant);
            _bddContext.SaveChanges();
            return enseignant.Id;
        }

        public int CreerEnseignant(Enseignant enseignant)
        {
            enseignant.Utilisateur.MotDePasse = UtilisateurServices.EncodeMD5(enseignant.Utilisateur.MotDePasse);
            _bddContext.Enseignants.Add(enseignant);
            _bddContext.SaveChanges();

            return enseignant.Id;
        }

        public void ModifierEnseignant(int id, string nom, string prenom, string identifiant, string motDePasse, TypeMatiere matiere, TypeNiveau niveau, int utilisateurID)
        {
            Enseignant enseignant = _bddContext.Enseignants.Find(id);
            enseignant.Utilisateur = _bddContext.Utilisateurs.Find(enseignant.UtilisateurId);

            if (enseignant != null)
            {
                enseignant.Utilisateur.Nom = nom;
                enseignant.Utilisateur.Prenom = prenom;
                enseignant.Utilisateur.Identifiant = identifiant;
                enseignant.Utilisateur.MotDePasse = motDePasse;
                enseignant.Matiere = matiere;
                enseignant.Niveaux= niveau;
               
                _bddContext.SaveChanges();
            }
        }

        public void ModifierEnseignant(Enseignant enseignant)
        {
            _bddContext.Enseignants.Update(enseignant);
            _bddContext.SaveChanges();
        }

        public void SupprimerEnseignant(int id)
        {
            Enseignant enseignant = _bddContext.Enseignants.Find(id);
            enseignant.Utilisateur = _bddContext.Utilisateurs.Find(enseignant.UtilisateurId);
            _bddContext.Enseignants.Remove(enseignant);
           _bddContext.Utilisateurs.Remove(enseignant.Utilisateur);
            _bddContext.SaveChanges();
        }


        public List<Enseignant> ObtientTousLesEnseignants()
        {
            List<Enseignant>listeEnseignants = _bddContext.Enseignants.ToList();
            foreach(Enseignant enseignant in listeEnseignants) 
            {
                enseignant.Utilisateur = _bddContext.Utilisateurs.Find(enseignant.UtilisateurId);
            }
            return listeEnseignants;
        }

        public Enseignant TrouverUnEnseignant(int id)
        {
            Enseignant enseignant = _bddContext.Enseignants.Find(id);
            enseignant.Utilisateur = _bddContext.Utilisateurs.Find(enseignant.UtilisateurId);

            return enseignant;
        }

        public int CompterEnseignant()
        {
            int nbEnseignant = _bddContext.Enseignants.Count();
            return nbEnseignant;
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }

}
