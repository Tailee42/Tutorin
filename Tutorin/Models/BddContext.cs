using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
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
        public DbSet<Gestionnaire> Gestionnaires { get; set; }
        public DbSet<PrestationEleve> PrestationsEleves { get; set; }

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
                new Utilisateur { Id = 8, Nom = "Pachere", Prenom = "Fraise", Identifiant = "pfraise", MotDePasse = UtilisateurServices.EncodeMD5("Fraise0") },
                new Utilisateur { Id = 9, Nom = "Legrand", Prenom = "Marc", Identifiant = "lmarc", MotDePasse = UtilisateurServices.EncodeMD5("Marco25") }

                );

            this.Gestionnaires.AddRange(
                new Gestionnaire { Id = 1, PosteOccupe = "Comptable",UtilisateurId =9}
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
                new Abonnement(TypeAbonnement.CoursEnLigne) { Id = 1, DateDebut = new System.DateTime(2022, 02, 23), ResponsableEleveId = 2, EleveId = 1 },
                new Abonnement(TypeAbonnement.Tutorat) { Id = 2, DateDebut = new System.DateTime(2022, 04, 10), ResponsableEleveId = 2 },
                new Abonnement(TypeAbonnement.Tutorat) { Id = 3, DateDebut = new System.DateTime(2021, 02, 08), DateFin = new System.DateTime(2022, 01, 10), ResponsableEleveId = 2, EleveId = 2 }
                );


            this.Enseignants.AddRange(
                new Enseignant{ Id = 1, Matiere = TypeMatiere.Mathematique, Niveaux = TypeNiveau.CM1, UtilisateurId = 5 },
                new Enseignant { Id = 2, Matiere = TypeMatiere.Physique, Niveaux = TypeNiveau.CM1, UtilisateurId = 6 },
                new Enseignant { Id = 3, Matiere = TypeMatiere.Geographie, Niveaux = TypeNiveau.Troisieme, UtilisateurId = 8 },
                new Enseignant { Id = 4, Matiere = TypeMatiere.Français, Niveaux = TypeNiveau.CP, UtilisateurId = 3}
                );

            this.ContenusPedagogiques.AddRange(
                new ContenuPedagogique { Id = 1, Matiere = TypeMatiere.Geographie, Niveau = TypeNiveau.Troisieme, Titre = "La France dans le Monde", DatePublication = new System.DateTime(2022, 05, 02), DateMiseAJour = new System.DateTime(2022, 08, 15), Etat = EtatContenuPedagogique.A_Valider, ContenuDuCours = "Dès le Moyen Âge, notre pays a largement influencé ses voisins européens (puis de nombreux pays à travers le monde) par sa politique, son économie, sa langue et sa culture.\r\nAvec l’Angleterre, la France a longtemps été en concurrence pour la domination du Monde.\r\nDepuis le siècle dernier, il est vrai que d’autres pays sont venus jouer les premiers rôles (Les Etats-Unis, la Chine, la Russie, …), mais la place de la France dans le Monde reste très importante. ", EnseignantId = 3},
                new ContenuPedagogique { Id = 2, Matiere = TypeMatiere.Physique, Niveau = TypeNiveau.CM1, Titre = "La lumière", DatePublication = new System.DateTime(2021, 10, 29), DateMiseAJour = new System.DateTime(2022, 01, 18), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "Sans lumière c'est tout noir.", EnseignantId = 2 },
                new ContenuPedagogique { Id = 3, Matiere = TypeMatiere.Mathematique, Niveau = TypeNiveau.CM1, Titre = "1+1=?", DatePublication = new System.DateTime(2022, 03, 11), Etat = EtatContenuPedagogique.A_Modifier, ContenuDuCours = "2 ! Bravo !", EnseignantId = 1 },
                new ContenuPedagogique { Id = 4, Matiere = TypeMatiere.Mathematique, Niveau = TypeNiveau.Terminale, Titre = "Primitives et équations différentielles", DatePublication = new System.DateTime(2022, 12, 11), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "Une équation différentielle est une équation dont l’inconnue est une fonction. Soit f une fonction définie sur un intervalle I de R. On dit que la fonction g est une solution de l’équation différentielle y’ = f sur I si et seulement si, g est dérivable sur I et, pour tout réel x de I, on a : g’(x) = f(x).", EnseignantId = 1 },
                new ContenuPedagogique { Id = 5, Matiere = TypeMatiere.Français, Niveau = TypeNiveau.CP, Titre = "Les verbes", DatePublication = new System.DateTime(2021, 08, 29), DateMiseAJour = new System.DateTime(2022, 04, 17), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "On distingue : Les verbes d'action : Un verbe d'action exprime une action faite ou subie par le sujet. Ex. : Marion prend un gâteau. Le bébé dort, il rêve. Le sapin est décoré. Les verbes d'état : Un verbe d'état (demeurer, devenir, paraître, rester, sembler) exprime un état du sujet. Ex. : Les invités semblaient heureux.", EnseignantId = 3 },
                new ContenuPedagogique { Id = 6, Matiere = TypeMatiere.Geographie, Niveau = TypeNiveau.Sixieme, Titre = "L'Union Européenne", DatePublication = new System.DateTime(2022, 01, 12), Etat = EtatContenuPedagogique.A_Modifier, ContenuDuCours = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In quis sapien et est vehicula euismod vitae ac mauris. Vivamus et justo mauris. Nullam augue sem, consectetur vitae euismod varius, placerat aliquet sem. Fusce eget euismod nulla. Quisque dapibus orci vitae nisl semper eleifend non in purus. Mauris tempor erat a leo dictum, ac pretium lacus molestie. Morbi sed nibh est. Cras nulla metus, iaculis quis rhoncus ut, ultricies sit amet metus. Nunc varius sagittis ipsum, sed tincidunt eros ultricies sit amet. Maecenas sed magna at ex commodo pretium. Aenean venenatis faucibus dolor, vulputate consectetur ante pharetra gravida. Pellentesque sed turpis mauris.", EnseignantId = 3 },
                new ContenuPedagogique { Id = 7, Matiere = TypeMatiere.Physique, Niveau = TypeNiveau.Seconde, Titre = "PH et titrage", DatePublication = new System.DateTime(2021, 11, 24), DateMiseAJour = new System.DateTime(2022, 12, 02), Etat = EtatContenuPedagogique.A_Publier, ContenuDuCours = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", EnseignantId = 2 },
                new ContenuPedagogique { Id = 8, Matiere = TypeMatiere.Français, Niveau = TypeNiveau.CE2, Titre = "L'imparfait et le passé composé", DatePublication = new System.DateTime(2021, 08, 29), DateMiseAJour = new System.DateTime(2022, 04, 17), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed iaculis tristique tellus, egestas feugiat nibh. Vivamus in vehicula turpis, sit amet blandit ligula. In viverra lacinia vestibulum. Aenean nec felis semper, imperdiet odio nec, molestie mauris. Aliquam magna nisl, blandit ac tincidunt at, finibus vel sapien. Suspendisse pretium tincidunt est, suscipit rutrum felis finibus vitae. In et lorem tristique risus blandit auctor id in magna.", EnseignantId = 3 }
                );

            this.SaveChanges();
        }
    }
}