using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Tutorin.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Utilisateur> Utilisateurs { get;set; }
        public DbSet<Enseignant> Enseignants { get; set; }

        public DbSet<ResponsableEleve> ResponsablesEleves { get;set; }

        public DbSet<Eleve> Eleves { get;set; }

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
                new Utilisateur { Id = 1, Nom = "Albrand", Prenom = "Pierre", Identifiant = "apierre", MotDePasse = "Pierre7" },
                new Utilisateur { Id = 2, Nom = "Roux", Prenom = "Louis", Identifiant = "rlouis", MotDePasse = "Louis12" },
                new Utilisateur { Id = 3, Nom = "Queyras", Prenom = "Antoine", Identifiant = "qantoine", MotDePasse = "Antoine9" },
                new Utilisateur { Id = 4, Nom = "Dupond", Prenom = "Anne", Identifiant = "danne", MotDePasse = "Anne456" },
                new Utilisateur { Id = 5, Nom = "Bonheur", Prenom = "Antoine", Identifiant = "bantoine", MotDePasse = "Antoine10" },
                new Utilisateur { Id = 6, Nom = "LOUISE", Prenom = "Anne", Identifiant = "lanne", MotDePasse = "Anne25" }
                );

            this.Eleves.AddRange(
                new Eleve { Id = 1, DateNaissance = new System.DateTime(2013, 07, 14), Niveau = TypeNiveau.CM1, UtilisateurID = 2 },
                new Eleve { Id = 2, DateNaissance = new System.DateTime(2011, 04, 01), Niveau = TypeNiveau.Sixieme, UtilisateurID = 4 }

                );

            this.Enseignants.AddRange(
                new Enseignant{ Id = 1, Matiere = TypeMatiere.Mathematique, Niveaux = TypeNiveau.CM1, UtilisateurId = 5 },
                new Enseignant { Id = 2, Matiere = TypeMatiere.Physique, Niveaux = TypeNiveau.CM1, UtilisateurId = 6 }

                );
            this.SaveChanges();
        }
    }
}