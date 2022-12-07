using System;
using System.Collections.Generic;
using Tutorin.Models;
using Tutorin.Services;
using Xunit;

namespace TestUnitaire
{
    public class UnitTestUtilisateur
    {
        [Fact]
        public void Creation_Utilisateur_Verification()
        {

            BddContext bdd = new BddContext();
            bdd.DeleteCreateDatabase();
            using (UtilisateurServices us = new UtilisateurServices())
            {

                us.CreerUtilisateur("Badaroux", "Antoine", "bantoine", "123456");
            }
            using (UtilisateurServices us = new UtilisateurServices())
            {
                List<Utilisateur> utilisateurs = us.ObtientTousLesUtilisateurs();
                Assert.Single(utilisateurs);
                Assert.Equal("Badaroux", utilisateurs[0].Nom);
                Assert.Equal("Antoine", utilisateurs[0].Prenom);
                Assert.Equal("bantoine", utilisateurs[0].Identifiant);
                Assert.Equal("123456", utilisateurs[0].MotDePasse);
            }
        }

        [Fact]
        public void Modification_Utilisateur_Verification()
        {
            BddContext bdd = new BddContext();
            bdd.DeleteCreateDatabase();

            using (UtilisateurServices us = new UtilisateurServices())
            {

                int id = us.CreerUtilisateur("Badaroux", "Antoine", "bantoine", "123456");
                us.ModifierUtilisateur(id, "Pasquali", "Antoine", "pantoine", "123456");
            }

            using (UtilisateurServices us = new UtilisateurServices())
            {
                List<Utilisateur> utilisateurs = us.ObtientTousLesUtilisateurs();
                Assert.Single(utilisateurs);
                Assert.Equal("Pasquali", utilisateurs[0].Nom);
                Assert.Equal("Antoine", utilisateurs[0].Prenom);
                Assert.Equal("pantoine", utilisateurs[0].Identifiant);
                Assert.Equal("123456", utilisateurs[0].MotDePasse);
            }
        }
        [Fact]
        public void Suppression_Utilisateur_Verification()
        {

            BddContext bdd = new BddContext();
            bdd.DeleteCreateDatabase();


            using (UtilisateurServices us = new UtilisateurServices())
            {

                int id = us.CreerUtilisateur("Badaroux", "Alfonse", "balfonse", "azerty");
                us.SupprimerUtilisateur(id);
            }
            using (UtilisateurServices us = new UtilisateurServices())
            {
                List<Utilisateur> utilisateurs = us.ObtientTousLesUtilisateurs();
                Assert.Empty(utilisateurs);
            }
        }
    }
}
