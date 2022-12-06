using System;
using System.Collections.Generic;
using Xunit;

namespace TestUnitaire
{
    public class UnitTestUtilisateur
    {
        [Fact]
        public void Creation_Utilisateur_Verification()
        {
            using (UtilisateurServices dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                dal.CreerUtilisateur("Badaroux", "Antoine", "bantoine", "123456");
            }
            using (Dal dal = new Dal())
            {
                List<Utilisateur> utilisateurs = dal.ObtientTousLesUtilisateurs();
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
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                int id = dal.CreerUtilisateur("Badaroux", "Antoine", "bantoine", "123456");
                dal.ModiferUtilisateur(id, "Pasquali", "Antoine", "pantoine", "123456");
            }
            using (Dal dal = new Dal())
            {
                List<Utilisateur> utilisateurs = dal.ObtientTousLesUtilisateurs();
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
            using (Dal dal = new Dal())
            {
                dal.DeleteCreateDatabase();
                int id = dal.CreerUtilisateur("Badaroux", "Antoine", "bantoine", "123456");
                dal.SupprimerUtilisateur(id);
            }
            using (Dal dal = new Dal())
            {
                List<Utilisateur> utilisateurs = dal.ObtientTousLesUtilisateurs();
                Assert.Empty(utilisateurs);
            }
        }
    }
}
