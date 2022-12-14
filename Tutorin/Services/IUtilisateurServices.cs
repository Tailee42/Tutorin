using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IUtilisateurServices : IDisposable
    {
        int CreerUtilisateur(string nom, string prenom, string identifiant, string motDePasse);
        void ModifierUtilisateur(int id, string nom, string prenom, string identifiant, string motDePasse);
        void SupprimerUtilisateur(int id);
        List<Utilisateur> ObtientTousLesUtilisateurs();
        int AjouterUtilisateur(string nom, string prenom, string identifiant, string motDePasse);
        Utilisateur Authentifier(string identifiant, string motDePasse);
        Utilisateur ObtenirUtilisateur(int id);
        Utilisateur ObtenirUtilisateur(string idStr);
        int CompterUtilisateur();
    }
}
