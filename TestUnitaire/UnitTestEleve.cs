using System;
using System.Collections.Generic;
using System.Text;
using Tutorin.Models;
using Tutorin.Services;
using Xunit;

namespace TestUnitaire
{
    public class UnitTestEleve
    {
        [Fact]
        public void Creation_Eleve_Verification()
        {

            BddContext bdd = new BddContext();
            bdd.DeleteCreateDatabase();

            using (EleveServices es = new EleveServices())
            {
             
                int utilisateurid;
                using(UtilisateurServices us = new UtilisateurServices())
                {
                    utilisateurid = us.CreerUtilisateur("Badaroux", "Antoine", "bantoine", "123456");
                    es.CreerEleve(new DateTime(2010, 7, 14), TypeNiveau.Quatrieme, utilisateurid);
                }
            }
            using (UtilisateurServices us = new UtilisateurServices())
            {
                using (EleveServices es = new EleveServices())
                {
                    List<Eleve> eleves = es.ObtientTousLesEleves();
                    Assert.Single(eleves);
                    Assert.Equal(new DateTime(2010, 7, 14), eleves[0].DateNaissance);
                    Assert.Equal(TypeNiveau.Quatrieme, eleves[0].Niveau);
                    Assert.Equal(1, eleves[0].UtilisateurId);
                }
               
            }
        }

        [Fact]
        public void Modification_Eleve_Verification()
        {

            BddContext bdd = new BddContext();
            bdd.DeleteCreateDatabase();

            using (EleveServices es = new EleveServices())
            {
                
                int utilisateurid;
                using (UtilisateurServices us = new UtilisateurServices())
                {
                    utilisateurid = us.CreerUtilisateur("Badaroux", "Antoine", "bantoine", "123456");
                    int eleveId = es.CreerEleve(new DateTime(2010, 7, 14), TypeNiveau.Quatrieme, utilisateurid);
                    es.ModifierEleve(eleveId, new DateTime(2012, 7, 14), TypeNiveau.Sixieme);
                }
            }

            using (EleveServices es = new EleveServices())
            {
                List<Eleve> eleves = es.ObtientTousLesEleves();
                Assert.Single(eleves);
                Assert.Equal(new DateTime(2012, 7, 14), eleves[0].DateNaissance);
                Assert.Equal(TypeNiveau.Sixieme, eleves[0].Niveau);
                Assert.Equal(1, eleves[0].UtilisateurId);
            }
        }
        [Fact]
        public void Suppression_Utilisateur_Verification()
        {

            BddContext bdd = new BddContext();
            bdd.DeleteCreateDatabase();

            using (EleveServices el = new EleveServices())
            {
                
                int utilisateurid;
                using (UtilisateurServices us = new UtilisateurServices())
                {
                    utilisateurid = us.CreerUtilisateur("Badaroux", "Antoine", "bantoine", "123456");
                    int eleveId = el.CreerEleve(new DateTime(2010, 7, 14), TypeNiveau.Quatrieme, utilisateurid);
                    el.SupprimerEleve(eleveId);
                    us.SupprimerUtilisateur(utilisateurid);
                }
            }
            
            using (UtilisateurServices us = new UtilisateurServices())
            {
                List<Utilisateur> utilisateurs = us.ObtientTousLesUtilisateurs();
                Assert.Empty(utilisateurs);
            }
            using (EleveServices es = new EleveServices())
            {
                List<Eleve> eleves = es.ObtientTousLesEleves();
                Assert.Empty(eleves);
            }
        }
    }
}
