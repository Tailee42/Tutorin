using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Tutorin.Services;
using Microsoft.Extensions.Configuration;
using System;

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
        public DbSet<Gestionnaire> Gestionnaires { get; set; }
        public DbSet<PrestationEleve> PrestationsEleves { get; set; }
        public DbSet<PrestationPayement> PrestationsPayements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
			if (System.Diagnostics.Debugger.IsAttached)
            {
               optionsBuilder.UseMySql("server=localhost;user id=root;password=rrrrr;database=tutorin");  // connexion string. Attention au password. avec comme nom de BDD : ChoixSejourTest
            }
            else
            {
                IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
                optionsBuilder.UseMySql(configuration.GetConnectionString("DefaultConnection"));
            }
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
                new Utilisateur { Id = 6, Nom = "Louise", Prenom = "Anne", Identifiant = "lanne", MotDePasse = UtilisateurServices.EncodeMD5("Anne0025") },
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
                new Prestation
                {
                    Id = 1,
                    Niveau = TypeNiveau.CM2,
                    DateDebut = new System.DateTime(2022, 12, 18, 14, 0, 0),
                    DateFin = new System.DateTime(2022, 12, 18, 14, 0, 0),
                    TypePrestation = TypePrestation.Tutorat,
                    Ville = "Paris",
                    Prix = 0,
                    Presentiel = true,
                    EnseignantId = 2,
                    EtatPrestation = EtatPrestation.Realisée
                },
                new Prestation
                {
                    Id = 2,
                    Niveau = TypeNiveau.Troisieme,
                    DateDebut = new System.DateTime(2022, 12, 30, 16, 0, 0),
                    DateFin = new System.DateTime(2022, 12, 30, 16, 0, 0),
                    TypePrestation = TypePrestation.Aide_aux_devoirs,
                    Prix = 0,
                    Matiere = TypeMatiere.Histoire,
                    Presentiel = false,
                    EnseignantId = 3,
                    EtatPrestation = EtatPrestation.Enseignants_inscrits
                },
                new Prestation
                {
                    Id = 3,
                    Niveau = TypeNiveau.Terminale,
                    DateDebut = new System.DateTime(2022, 12, 27, 14, 0, 0),
                    DateFin = new System.DateTime(2022, 12, 27, 14, 0, 0),
                    TypePrestation = TypePrestation.Stage_de_revision,
                    Ville = "Paris",
                    Prix = 25F,
                    Matiere = TypeMatiere.Mathematiques,
                    Presentiel = true,
                    EtatPrestation = EtatPrestation.A_affecter
                },
                new Prestation
                {
                    Id = 4,
                    Niveau = TypeNiveau.CM1,
                    DateDebut = new System.DateTime(2022, 12, 23, 14, 0, 0),
                    DateFin = new System.DateTime(2022, 12, 23, 15, 0, 0),
                    TypePrestation = TypePrestation.Tutorat,
                    Ville = "Paris",
                    Prix = 0,
                    Presentiel = true,
                    EnseignantId = 2,
                    EtatPrestation = EtatPrestation.Payee_par_responsable_eleve
                },
                new Prestation
                {
                    Id = 5,
                    Niveau = TypeNiveau.Sixieme,
                    DateDebut = new System.DateTime(2022, 12, 06, 17, 0, 0),
                    DateFin = new System.DateTime(2022, 12, 06, 18, 0, 0),
                    TypePrestation = TypePrestation.Aide_aux_devoirs,
                    Prix = 0,
                    Matiere = TypeMatiere.Mathematiques,
                    Presentiel = false,
                    LienVisio = "zoom.us",
                    EnseignantId = 1,
                    EtatPrestation = EtatPrestation.Payee_par_responsable_eleve
                },
                new Prestation
                {
                    Id = 6,
                    Niveau = TypeNiveau.CM1,
                    DateDebut = new System.DateTime(2023, 01, 12, 17, 0, 0),
                    DateFin = new System.DateTime(2023, 01, 12, 18, 30, 0),
                    TypePrestation = TypePrestation.Aide_aux_devoirs,
                    Prix = 0,
                    Matiere = TypeMatiere.Histoire,
                    Presentiel = false,
                    LienVisio = "zoom.us",
                    EnseignantId = 3,
                    EtatPrestation = EtatPrestation.Payee_par_responsable_eleve
                },
                new Prestation
                {
                    Id = 7,
                    Niveau = TypeNiveau.CM1,
                    DateDebut = new System.DateTime(2023, 01, 30, 17, 30, 0),
                    DateFin = new System.DateTime(2023, 01, 30, 19, 00, 0),
                    TypePrestation = TypePrestation.Cours_particulier,
                    Prix = 20F,
                    Matiere = TypeMatiere.Mathematiques,
                    Presentiel = false,
                    LienVisio = "zoom.us",
                    EnseignantId = 1,
                    EtatPrestation = EtatPrestation.Enseignants_inscrits
                },
                new Prestation
                {
                    Id = 8,
                    Niveau = TypeNiveau.Sixieme,
                    DateDebut = new System.DateTime(2023, 01, 05, 18, 00, 0),
                    DateFin = new System.DateTime(2023, 01, 05, 20, 00, 0),
                    TypePrestation = TypePrestation.Cours_particulier,
                    Prix = 20F,
                    Matiere = TypeMatiere.Français,
                    Presentiel = false,
                    LienVisio = "zoom.us",
                    EnseignantId = 4,
                    EtatPrestation = EtatPrestation.Enseignants_inscrits
                },
                new Prestation
                {
                    Id = 9,
                    Niveau = TypeNiveau.Premiere,
                    DateDebut = new System.DateTime(2023, 01, 26, 18, 0, 0),
                    DateFin = new System.DateTime(2023, 01, 26, 19, 0, 0),
                    TypePrestation = TypePrestation.Tutorat,
                    Prix = 0,
                    Presentiel = false,
                    LienVisio = "zoom.us",
                    EnseignantId = 4,
                    EtatPrestation = EtatPrestation.Enseignants_inscrits
                },
                new Prestation
                {
                    Id = 10,
                    Niveau = TypeNiveau.Terminale,
                    DateDebut = new System.DateTime(2023, 02, 02, 17, 0, 0),
                    DateFin = new System.DateTime(2023, 02, 02, 19, 0, 0),
                    TypePrestation = TypePrestation.Aide_aux_devoirs,
                    Prix = 0,
                    Matiere = TypeMatiere.Chimie,
                    Presentiel = false,
                    LienVisio = "zoom.us",
                    EtatPrestation = EtatPrestation.A_affecter
                },
                new Prestation
                {
                    Id = 11,
                    Niveau = TypeNiveau.CM2,
                    DateDebut = new System.DateTime(2023, 01, 23, 18, 0, 0),
                    DateFin = new System.DateTime(2023, 01, 23, 19, 0, 0),
                    TypePrestation = TypePrestation.Cours_particulier,
                    Prix = 20F,
                    Matiere = TypeMatiere.Français,
                    Presentiel = false,
                    LienVisio = "zoom.us",
                    EnseignantId = 4,
                    EtatPrestation = EtatPrestation.Enseignants_inscrits
                },
                new Prestation
                {
                    Id = 12,
                    Niveau = TypeNiveau.CE1,
                    DateDebut = new System.DateTime(2023, 02, 08, 17, 30, 0),
                    DateFin = new System.DateTime(2023, 02, 08, 19, 30, 0),
                    TypePrestation = TypePrestation.Aide_aux_devoirs,
                    Prix = 0,
                    Matiere = TypeMatiere.Mathematiques,
                    Presentiel = false,
                    LienVisio = "zoom.us",
                    EnseignantId = 1,
                    EtatPrestation = EtatPrestation.Enseignants_inscrits
                },
                new Prestation
                {
                    Id = 13,
                    Niveau = TypeNiveau.Quatrieme,
                    DateDebut = new System.DateTime(2023, 02, 27, 17, 0, 0),
                    DateFin = new System.DateTime(2023, 02, 27, 19, 0, 0),
                    TypePrestation = TypePrestation.Tutorat,
                    Prix = 0,
                    Presentiel = false,
                    LienVisio = "zoom.us",
                    EtatPrestation = EtatPrestation.A_affecter
                }
                );

            this.ResponsablesEleves.AddRange(
                new ResponsableEleve { Id = 1, Mail = "slegrand@gmail.com", UtilisateurId = 7 },
                new ResponsableEleve { Id = 2, Mail = "palbrand@gmail.com", UtilisateurId = 1 }

                );

            this.Abonnements.AddRange(
                new Abonnement(TypeAbonnement.CoursEnLigne) { Id = 1, DateDebut = new System.DateTime(2022, 02, 23), ResponsableEleveId = 2, PayementId = 2 },
                new Abonnement(TypeAbonnement.Tutorat) { Id = 2, DateDebut = new System.DateTime(2022, 04, 10), ResponsableEleveId = 2, EleveId = 1, PayementId = 3 },
                new Abonnement(TypeAbonnement.Tutorat) { Id = 3, DateDebut = new System.DateTime(2021, 02, 08), DateFin = new System.DateTime(2022, 01, 10), ResponsableEleveId = 2, EleveId = 2, PayementId = 1 }
                );


            this.Enseignants.AddRange(
                new Enseignant{ Id = 1, Matiere = TypeMatiere.Mathematiques, Niveaux = TypeNiveau.CM1, UtilisateurId = 5 , ImagePath = "/images/Bonheur.jpg" },
                new Enseignant { Id = 2, Matiere = TypeMatiere.Physique, Niveaux = TypeNiveau.CM1, UtilisateurId = 6, ImagePath = "/images/Louise.jpg" },
                new Enseignant { Id = 3, Matiere = TypeMatiere.Geographie, Niveaux = TypeNiveau.Troisieme, UtilisateurId = 8, ImagePath = "/images/Pachere.jpg" },
                new Enseignant { Id = 4, Matiere = TypeMatiere.Français, Niveaux = TypeNiveau.CP, UtilisateurId = 3, ImagePath = "/images/Queyras.jpg" }
                );

            this.ContenusPedagogiques.AddRange(
                new ContenuPedagogique { Id = 1, Matiere = TypeMatiere.Geographie, Niveau = TypeNiveau.Troisieme, Titre = "La France dans le Monde", DatePublication = new System.DateTime(2022, 05, 02), DateMiseAJour = new System.DateTime(2022, 08, 15), Etat = EtatContenuPedagogique.A_Valider, ContenuDuCours = "Dès le Moyen Âge, notre pays a largement influencé ses voisins européens (puis de nombreux pays à travers le monde) par sa politique, son économie, sa langue et sa culture.\r\nAvec l’Angleterre, la France a longtemps été en concurrence pour la domination du Monde.\r\nDepuis le siècle dernier, il est vrai que d’autres pays sont venus jouer les premiers rôles (Les Etats-Unis, la Chine, la Russie, …), mais la place de la France dans le Monde reste très importante. ", EnseignantId = 3},
                new ContenuPedagogique { Id = 2, Matiere = TypeMatiere.Physique, Niveau = TypeNiveau.CM1, Titre = "La lumière", DatePublication = new System.DateTime(2021, 10, 29), DateMiseAJour = new System.DateTime(2022, 01, 18), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "Sans lumière, c'est tout noir.", EnseignantId = 2 },
                new ContenuPedagogique { Id = 3, Matiere = TypeMatiere.Mathematiques, Niveau = TypeNiveau.CM1, Titre = "1+1=?", DatePublication = new System.DateTime(2022, 03, 11), Etat = EtatContenuPedagogique.A_Modifier, ContenuDuCours = "2 ! Bravo !", EnseignantId = 1 },
                new ContenuPedagogique { Id = 4, Matiere = TypeMatiere.Mathematiques, Niveau = TypeNiveau.Terminale, Titre = "Primitives et équations différentielles", DatePublication = new System.DateTime(2022, 12, 11), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "Une équation différentielle est une équation dont l’inconnue est une fonction. <br> Soit f une fonction définie sur un intervalle I de R. <br> On dit que la fonction g est une solution de l’équation différentielle y’ = f sur I si et seulement si, g est dérivable sur I et, pour tout réel x de I, on a : g’(x) = f(x).", EnseignantId = 1 },
                new ContenuPedagogique { Id = 5, Matiere = TypeMatiere.Français, Niveau = TypeNiveau.CP, Titre = "Les verbes", DatePublication = new System.DateTime(2021, 08, 29), DateMiseAJour = new System.DateTime(2022, 04, 17), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "On distingue : <br/> <strong>Les verbes d'action :</strong> Un verbe d'action exprime une action faite ou subie par le sujet.<br> Ex. : Marion prend un gâteau. Le bébé dort, il rêve. Le sapin est décoré.<br> <strong>Les verbes d'état :</strong> Un verbe d'état (demeurer, devenir, paraître, rester, sembler) exprime un état du sujet. <br> Ex. : Les invités semblaient heureux.", EnseignantId = 3 },
                new ContenuPedagogique { Id = 6, Matiere = TypeMatiere.Geographie, Niveau = TypeNiveau.Sixieme, Titre = "L'Union Européenne", DatePublication = new System.DateTime(2022, 01, 12), Etat = EtatContenuPedagogique.A_Modifier, ContenuDuCours = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In quis sapien et est vehicula euismod vitae ac mauris. Vivamus et justo mauris. Nullam augue sem, consectetur vitae euismod varius, placerat aliquet sem. Fusce eget euismod nulla. Quisque dapibus orci vitae nisl semper eleifend non in purus. Mauris tempor erat a leo dictum, ac pretium lacus molestie. Morbi sed nibh est. Cras nulla metus, iaculis quis rhoncus ut, ultricies sit amet metus. Nunc varius sagittis ipsum, sed tincidunt eros ultricies sit amet. Maecenas sed magna at ex commodo pretium. Aenean venenatis faucibus dolor, vulputate consectetur ante pharetra gravida. Pellentesque sed turpis mauris.", EnseignantId = 3 },
                new ContenuPedagogique { Id = 7, Matiere = TypeMatiere.Physique, Niveau = TypeNiveau.Seconde, Titre = "PH et titrage", DatePublication = new System.DateTime(2021, 11, 24), DateMiseAJour = new System.DateTime(2022, 12, 02), Etat = EtatContenuPedagogique.A_Valider, ContenuDuCours = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", EnseignantId = 2 },
                new ContenuPedagogique { Id = 8, Matiere = TypeMatiere.Français, Niveau = TypeNiveau.CE2, Titre = "L'imparfait et le passé composé", DatePublication = new System.DateTime(2021, 08, 29), DateMiseAJour = new System.DateTime(2022, 04, 17), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed iaculis tristique tellus, egestas feugiat nibh. Vivamus in vehicula turpis, sit amet blandit ligula. In viverra lacinia vestibulum. Aenean nec felis semper, imperdiet odio nec, molestie mauris. Aliquam magna nisl, blandit ac tincidunt at, finibus vel sapien. Suspendisse pretium tincidunt est, suscipit rutrum felis finibus vitae. In et lorem tristique risus blandit auctor id in magna.", EnseignantId = 3 },
                new ContenuPedagogique { Id = 9, Matiere = TypeMatiere.Chimie, Niveau = TypeNiveau.Quatrieme, Titre = "Les transformations chimiques", DatePublication = new System.DateTime(2022, 12, 16), DateMiseAJour = new System.DateTime(2022, 12, 17), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "", EnseignantId = 2 },
                new ContenuPedagogique { Id = 10, Matiere = TypeMatiere.Histoire, Niveau = TypeNiveau.Premiere, Titre = "La guerre 1914-1918", DatePublication = new System.DateTime(2022, 11, 10), Etat = EtatContenuPedagogique.A_Modifier, ContenuDuCours = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. In quis sapien et est vehicula euismod vitae ac mauris. Vivamus et justo mauris. Nullam augue sem, consectetur vitae euismod varius, placerat aliquet sem. Fusce eget euismod nulla. Quisque dapibus orci vitae nisl semper eleifend non in purus. Mauris tempor erat a leo dictum, ac pretium lacus molestie. Morbi sed nibh est. Cras nulla metus, iaculis quis rhoncus ut, ultricies sit amet metus. Nunc varius sagittis ipsum, sed tincidunt eros ultricies sit amet. Maecenas sed magna at ex commodo pretium. Aenean venenatis faucibus dolor, vulputate consectetur ante pharetra gravida. Pellentesque sed turpis mauris.", EnseignantId = 4 },
                new ContenuPedagogique { Id = 11, Matiere = TypeMatiere.Anglais, Niveau = TypeNiveau.CM2, Titre = "Le vocabulaire du petit-déjeuner", DatePublication = new System.DateTime(2021, 09, 10), DateMiseAJour = new System.DateTime(2022, 12, 16), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "", EnseignantId = 4 },
                new ContenuPedagogique { Id = 12, Matiere = TypeMatiere.Anglais, Niveau = TypeNiveau.Cinquieme, Titre = "Le prétérit simple", DatePublication = new System.DateTime(2022, 12, 09), DateMiseAJour = new System.DateTime(2022, 12, 15), Etat = EtatContenuPedagogique.En_Ligne, ContenuDuCours = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed iaculis tristique tellus, egestas feugiat nibh. Vivamus in vehicula turpis, sit amet blandit ligula. In viverra lacinia vestibulum. Aenean nec felis semper, imperdiet odio nec, molestie mauris. Aliquam magna nisl, blandit ac tincidunt at, finibus vel sapien. Suspendisse pretium tincidunt est, suscipit rutrum felis finibus vitae. In et lorem tristique risus blandit auctor id in magna.", EnseignantId = 4 }
                );

            this.Payements.AddRange(
                new Payement { Id = 1, MontantTTC = TypeAbonnementExtensions.PrixTTCAbonnement(TypeAbonnement.Tutorat), DatePayement = new System.DateTime(2021, 02, 08, 15, 21,12), NomTitulaireCarte = "Albrand", NumeroCarte = "1111222233334444", DateExpiration = "01/22", CVC = "741", ResponsableEleveId = 2},
                new Payement { Id = 2, MontantTTC = TypeAbonnementExtensions.PrixTTCAbonnement(TypeAbonnement.CoursEnLigne), DatePayement = new System.DateTime(2022, 02, 23, 9, 10, 0), NomTitulaireCarte = "Albrand", NumeroCarte = "1111222233334444", DateExpiration = "03/24", CVC = "123", ResponsableEleveId = 2 },
                new Payement { Id = 3, MontantTTC = TypeAbonnementExtensions.PrixTTCAbonnement(TypeAbonnement.Tutorat), DatePayement = new System.DateTime(2022, 04, 10, 9, 10, 0), NomTitulaireCarte = "Albrand", NumeroCarte = "1111222233334444", DateExpiration = "03/24", CVC = "123", ResponsableEleveId = 2 },
                new Payement { Id = 4, MontantTTC = 25F, DatePayement = new System.DateTime(2022, 12, 3, 15, 15, 0), NomTitulaireCarte = "Albrand", NumeroCarte = "1111222233334444", DateExpiration = "03/24", CVC = "123", ResponsableEleveId = 2 }
                );

            this.PrestationsEleves.AddRange(
                new PrestationEleve { Id = 1, EleveId = 1, PrestationId = 4},
                new PrestationEleve { Id = 2, EleveId = 2, PrestationId = 5},
                new PrestationEleve { Id = 3, EleveId = 1, PrestationId = 6},
                new PrestationEleve { Id = 4, EleveId = 1, PrestationId = 7}
                );

            this.PrestationsPayements.AddRange(
                new PrestationPayement { Id = 1, PrestationId = 4, PayementId = 4}
                );

            this.SaveChanges();
        }
    }
}