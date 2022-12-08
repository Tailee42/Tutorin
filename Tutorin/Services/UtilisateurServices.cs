using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tutorin.Models;
using System.Security.Cryptography;


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

        public int AjouterUtilisateur(string nom, string prenom, string identifiant, string motDePasse)
        {
            string motDePasseCode = EncodeMD5(motDePasse);
            Utilisateur user = new Utilisateur() {Nom= nom, Prenom = prenom, MotDePasse = motDePasseCode };
            this._bddContext.Utilisateurs.Add(user);
            this._bddContext.SaveChanges();
            return user.Id;
        }

        public Utilisateur Authentifier(string identifiant, string motDePasse)
        {
            {
                string motDePasseCode = EncodeMD5(motDePasse);
                Utilisateur user = this._bddContext.Utilisateurs.FirstOrDefault(u => u.Identifiant == identifiant && u.MotDePasse == motDePasseCode);
                return user;
            }
         
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return this._bddContext.Utilisateurs.Find(id);
        }

        public Utilisateur ObtenirUtilisateur(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
            {
                return this.ObtenirUtilisateur(id);
            }
            return null;
        }
        private string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "ChoixResto" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }

    }
}
