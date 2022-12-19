using System.Collections.Generic;
using System;
using Tutorin.Models;
using System.Linq;

namespace Tutorin.Services
{
    public class GestionnaireServices : IGestionnaireServices
    {
        private BddContext _bddContext;

        public GestionnaireServices()
        {
            _bddContext = new BddContext();
        }

        public int CreerGestionnaire(string posteOccupe, int utilisateurId)
        {
            Gestionnaire gestionnaire = new Gestionnaire() {PosteOccupe =posteOccupe, UtilisateurId = utilisateurId };
            _bddContext.Gestionnaires.Add(gestionnaire);
            _bddContext.SaveChanges();

            return gestionnaire.Id;
        }

        public int CreerGestionnaire(Gestionnaire gestionnaire)
        {
            gestionnaire.Utilisateur.MotDePasse = UtilisateurServices.EncodeMD5(gestionnaire.Utilisateur.MotDePasse);
            _bddContext.Gestionnaires.Add(gestionnaire);
            _bddContext.SaveChanges();

            return gestionnaire.Id;
        }

        public void ModifierGestionnaire(int id, string nom, string prenom, string identifiant, string posteOccupe)
        {
            Gestionnaire gestionnaire = _bddContext.Gestionnaires.Find(id);
            gestionnaire.Utilisateur = _bddContext.Utilisateurs.Find(gestionnaire.UtilisateurId);
            if (gestionnaire != null)
            {
                gestionnaire.Utilisateur.Nom = nom;
                gestionnaire.Utilisateur.Prenom = prenom;
                gestionnaire.Utilisateur.Identifiant = identifiant;
                gestionnaire.PosteOccupe = posteOccupe;
                _bddContext.SaveChanges();
            }
        }

        public void ModifierGestionnaire(Gestionnaire gestionnaire)
        {
            gestionnaire.Utilisateur.MotDePasse = UtilisateurServices.EncodeMD5(gestionnaire.Utilisateur.MotDePasse);
            _bddContext.Gestionnaires.Update(gestionnaire);
            _bddContext.SaveChanges();
        }

        public void ModifierMotdePasse(Gestionnaire gestionnaire, string ancienMdp, string newMdp, string confirmMdp)
        {
            ancienMdp = UtilisateurServices.EncodeMD5(ancienMdp);
            newMdp = UtilisateurServices.EncodeMD5(newMdp);
            confirmMdp = UtilisateurServices.EncodeMD5(confirmMdp);
            if (ancienMdp == gestionnaire.Utilisateur.MotDePasse)
            {
                if (newMdp == confirmMdp)
                {
                    gestionnaire.Utilisateur.MotDePasse = newMdp;
                    _bddContext.Utilisateurs.Update(gestionnaire.Utilisateur);
                    _bddContext.SaveChanges();
                }
            }
        }

        public List<Gestionnaire> ObtientTousLesGestionnaires()
        {
            List<Gestionnaire> listeGestionnaires = _bddContext.Gestionnaires.ToList();
            foreach (Gestionnaire gestionnaire in listeGestionnaires)
            {
                gestionnaire.Utilisateur = _bddContext.Utilisateurs.Find(gestionnaire.UtilisateurId);
            }

            return listeGestionnaires;
        }

        public void SupprimerGestionnaire(int id)
        {
            Gestionnaire gestionnaire = _bddContext.Gestionnaires.Find(id);
            gestionnaire.Utilisateur = _bddContext.Utilisateurs.Find(gestionnaire.UtilisateurId);
            _bddContext.Gestionnaires.Remove(gestionnaire);
            _bddContext.Utilisateurs.Remove(gestionnaire.Utilisateur);
            _bddContext.SaveChanges();
        }

        public Gestionnaire TrouverUnGestionnaire(int id)
        {
            Gestionnaire gestionnaire = _bddContext.Gestionnaires.Find(id);
            gestionnaire.Utilisateur = _bddContext.Utilisateurs.Find(gestionnaire.UtilisateurId);

            return gestionnaire;
        }

        public int CompterGestionnaire()
        {
            int nbGestionnaire = _bddContext.Gestionnaires.Count();
            return nbGestionnaire;
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }

}
