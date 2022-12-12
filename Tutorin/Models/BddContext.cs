using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Tutorin.Services;

namespace Tutorin.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get;set; }
        public DbSet<Enseignant> Enseignants { get; set; }
        public DbSet<ResponsableEleve> ResponsablesEleves { get;set; }
        public DbSet<Abonnement> Abonnements { get; set; }
        public DbSet<Eleve> Eleves { get;set; }
        public DbSet<Prestation> Prestations { get; set; }
        public DbSet<ContenuPedagogique> ContenusPedagogiques { get; set; }
        public DbSet<Payement> Payements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user id=root;password=rrrrr;database=tutorin");
        }

        public void DeleteCreateDatabase()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public void InitializaDb()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();

            this.Utilisateurs.AddRange(

                new Utilisateur { Id = 1, Nom = "Albrand", Prenom = "Pierre", Identifiant = "apierre", MotDePasse = UtilisateurServices.EncodeMD5("Pierre7") },
                new Utilisateur { Id = 2, Nom = "Roux", Prenom = "Louis", Identifiant = "rlouis", MotDePasse = UtilisateurServices.EncodeMD5("Louis12") },
                new Utilisateur { Id = 3, Nom = "Queyras", Prenom = "Antoine", Identifiant = "qantoine", MotDePasse = UtilisateurServices.EncodeMD5("Antoine9") },
                new Utilisateur { Id = 4, Nom = "Dupond", Prenom = "Anne", Identifiant = "danne", MotDePasse = UtilisateurServices.EncodeMD5("Anne456") },
                new Utilisateur { Id = 5, Nom = "Bonheur", Prenom = "Antoine", Identifiant = "bantoine", MotDePasse = UtilisateurServices.EncodeMD5("Antoine10") },
                new Utilisateur { Id = 6, Nom = "Louise", Prenom = "Anne", Identifiant = "lanne", MotDePasse = UtilisateurServices.EncodeMD5("Anne25") },
                new Utilisateur { Id = 7, Nom = "Legrand", Prenom = "Susie", Identifiant = "lsusie", MotDePasse = UtilisateurServices.EncodeMD5("Susie33")},
                new Utilisateur { Id = 8, Nom = "Pachere", Prenom = "Fraise", Identifiant = "pfraise", MotDePasse = UtilisateurServices.EncodeMD5("Fraise0") }

                );

            this.Eleves.AddRange(
                new Eleve { Id = 1, DateNaissance = new System.DateTime(2013, 07, 14), Niveau = TypeNiveau.CM1, UtilisateurId = 2 },
                new Eleve { Id = 2, DateNaissance = new System.DateTime(2011, 04, 01), Niveau = TypeNiveau.Sixieme, UtilisateurId = 4 }

                );

            this.Prestations.AddRange(
                new Prestation { Niveau = TypeNiveau.CM2, DateDebut = new System.DateTime(2022, 12, 06, 14,0,0), 
                    DateFin = new System.DateTime(2022, 12, 06, 14, 0, 0), TypePrestation = TypePrestation.Tuturat, Ville = "Paris",
                    Prix = 25F, Matiere = TypeMatiere.Physique, Presentiel = true}
                );

            this.ResponsablesEleves.AddRange(
                new ResponsableEleve { Id = 1, Mail = "slegrand@gmail.com", UtilisateurId = 7 },
                new ResponsableEleve { Id = 2, Mail = "palbrand@gmail.com", UtilisateurId = 1 }

                );

            this.Abonnements.AddRange(
                new Abonnement(TypeAbonnement.CoursEnLigne) { Id = 1, DateDebut = new System.DateTime(2022, 02, 23), ResponsableEleveId = 2, EleveId = 1, PayementId = 2 },
                new Abonnement(TypeAbonnement.Tutorat) { Id = 2, DateDebut = new System.DateTime(2022, 04, 10), ResponsableEleveId = 2, PayementId = 3 },
                new Abonnement(TypeAbonnement.Tutorat) { Id = 3, DateDebut = new System.DateTime(2021, 02, 08), DateFin = new System.DateTime(2022, 01, 10), ResponsableEleveId = 2, EleveId = 2, PayementId = 1 }
                );


            this.Enseignants.AddRange(
                new Enseignant{ Id = 1, Matiere = TypeMatiere.Mathematique, Niveaux = TypeNiveau.CM1, UtilisateurId = 5 },
                new Enseignant { Id = 2, Matiere = TypeMatiere.Physique, Niveaux = TypeNiveau.CM1, UtilisateurId = 6 },
                new Enseignant { Id = 3, Matiere = TypeMatiere.Geographie, Niveaux = TypeNiveau.Troisieme, UtilisateurId = 8 }

                );

            this.ContenusPedagogiques.AddRange(
                new ContenuPedagogique { Id = 1, Matiere = TypeMatiere.Geographie, Niveau = TypeNiveau.Troisieme, Titre = "La France dans le Monde", DatePublication = new System.DateTime(2022, 05, 02), DateMiseAJour = new System.DateTime(2022, 08, 15), Etat = EtatContenuPedagogique.A_Valider, ContenuDuCours = "Dès le Moyen Âge, notre pays a largement influencé ses voisins européens (puis de nombreux pays à travers le monde) par sa politique, son économie, sa langue et sa culture.\r\nAvec l’Angleterre, la France a longtemps été en concurrence pour la domination du Monde.\r\nDepuis le siècle dernier, il est vrai que d’autres pays sont venus jouer les premiers rôles (Les Etats-Unis, la Chine, la Russie, …), mais la place de la France dans le Monde reste très importante. ", EnseignantId = 3},
                new ContenuPedagogique { Id = 2, Matiere = TypeMatiere.Physique, Niveau = TypeNiveau.CM1, Titre = "La lumière", DatePublication = new System.DateTime(2021, 10, 29), DateMiseAJour = new System.DateTime(2022, 01, 18), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "Sans lumière c'est tout noir.", EnseignantId = 2 },
                new ContenuPedagogique { Id = 3, Matiere = TypeMatiere.Mathematique, Niveau = TypeNiveau.CM1, Titre = "1+1=?", DatePublication = new System.DateTime(2022, 03, 11), Etat = EtatContenuPedagogique.A_Modifier, ContenuDuCours = "2 ! Bravo !", EnseignantId = 1 }
                );

            this.Payements.AddRange(
                new Payement { Id = 1, MontantTTC = TypeAbonnementExtensions.PrixTTCAbonnement(TypeAbonnement.Tutorat), DatePayement = new System.DateTime(2021, 02, 08, 15, 21,12), NomTitulaireCarte = "Albrand", NumeroCarte = "1111222233334444", DateExpiration = "01/22", CVC = "741", ResponsableEleveId = 1},
                new Payement { Id = 2, MontantTTC = TypeAbonnementExtensions.PrixTTCAbonnement(TypeAbonnement.CoursEnLigne), DatePayement = new System.DateTime(2022, 02, 23, 9, 10, 0), NomTitulaireCarte = "Albrand", NumeroCarte = "1111222233334444", DateExpiration = "03/24", CVC = "123", ResponsableEleveId = 1 },
                new Payement { Id = 3, MontantTTC = TypeAbonnementExtensions.PrixTTCAbonnement(TypeAbonnement.Tutorat), DatePayement = new System.DateTime(2022, 04, 10, 9, 10, 0), NomTitulaireCarte = "Albrand", NumeroCarte = "1111222233334444", DateExpiration = "03/24", CVC = "123", ResponsableEleveId = 1 }
                );

            this.SaveChanges();
        }
    }
}