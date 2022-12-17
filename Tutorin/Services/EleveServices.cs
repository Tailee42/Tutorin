using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorin.Models;
using System.Security.Cryptography;

namespace Tutorin.Services
{
    public class EleveServices : IEleveServices
    {
        private BddContext _bddContext;

        public EleveServices()
        {
            _bddContext = new BddContext();
        }

        public int CreerEleve(DateTime dateNaissance, TypeNiveau niveau, int utilisateurID)
        {
            Eleve eleve = new Eleve() { DateNaissance = dateNaissance, Niveau = niveau, UtilisateurId = utilisateurID };
            _bddContext.Eleves.Add(eleve);
            _bddContext.SaveChanges();

            return eleve.Id;
        }

        public int CreerEleve(Eleve eleve)
        {
            _bddContext.Eleves.Add(eleve);
            _bddContext.SaveChanges();

            return eleve.Id;
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public void ModifierEleve(int id, string nom, string prenom, string identifiant, DateTime dateNaissance, TypeNiveau niveau)
        {
            Eleve eleve = _bddContext.Eleves.Find(id);
            eleve.Utilisateur = _bddContext.Utilisateurs.Find(eleve.UtilisateurId);
            if (eleve != null)
            {
                eleve.Utilisateur.Nom = nom;
                eleve.Utilisateur.Prenom = prenom;
                eleve.Utilisateur.Identifiant = identifiant;
                eleve.DateNaissance = dateNaissance;
                eleve.Niveau = niveau;
                _bddContext.Eleves.Update(eleve);
                _bddContext.SaveChanges();
            }
        }

        public void ModifierEleve(Eleve eleve)
        {
            eleve.Utilisateur.MotDePasse = UtilisateurServices.EncodeMD5(eleve.Utilisateur.MotDePasse);
            _bddContext.Eleves.Update(eleve);
            _bddContext.SaveChanges();
        }

        public void ModifierMotdePasse(Eleve eleve, string ancienMdp, string newMdp, string confirmMdp)
        {
            ancienMdp = UtilisateurServices.EncodeMD5(ancienMdp);
            if (ancienMdp == eleve.Utilisateur.MotDePasse)
            {
                if (newMdp == confirmMdp)
                {
                    eleve.Utilisateur.MotDePasse = UtilisateurServices.EncodeMD5(newMdp);
                    _bddContext.Utilisateurs.Update(eleve.Utilisateur);
                }
            }
            _bddContext.SaveChanges();
        }

        public List<Eleve> ObtientTousLesEleves()
        {
            List<Eleve> listeEleves = _bddContext.Eleves.ToList();
            foreach (Eleve eleve in listeEleves)
            {
                eleve.Utilisateur = _bddContext.Utilisateurs.Find(eleve.UtilisateurId);
            }

            return listeEleves;
        }

        public void SupprimerEleve(int id)
        {
            Eleve eleve = _bddContext.Eleves.Find(id);
            eleve.Utilisateur = _bddContext.Utilisateurs.Find(eleve.UtilisateurId);

            Abonnement abonnement = _bddContext.Abonnements.Where(a => a.EleveId == eleve.Id).FirstOrDefault();
            abonnement.EleveId = null;
            _bddContext.Abonnements.Update(abonnement);
            _bddContext.Eleves.Remove(eleve);
            _bddContext.Utilisateurs.Remove(eleve.Utilisateur);
            _bddContext.SaveChanges();
        }

        public Eleve TrouverUnEleve(int id)
        {
            Eleve eleve = _bddContext.Eleves.Find(id);
            eleve.Utilisateur = _bddContext.Utilisateurs.Find(eleve.UtilisateurId);

            return eleve;
        }

        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

        public int CompterEleve()
        {
            int nbEleve = _bddContext.Eleves.Count();
            return nbEleve;

        }
    }
    
}

