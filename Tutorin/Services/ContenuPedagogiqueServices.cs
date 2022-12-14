using System.Collections.Generic;
using System;
using Tutorin.Models;
using System.Linq;

namespace Tutorin.Services
{
    public class ContenuPedagogiqueServices : IContenuPedagogiqueServices
    {
        private BddContext _bddContext;

        public ContenuPedagogiqueServices()
        {
            _bddContext = new BddContext();
        }

        public int CreerContenuPedagogique(TypeMatiere matiere, TypeNiveau niveau, string titre, string contenu, int enseignantId)
        {
            //à sa création, le contenu a un état = a valider, et pas de date de publication ni de date de mise à jour
            ContenuPedagogique cours = new ContenuPedagogique()
            {
                Matiere = matiere,
                Niveau = niveau,
                Titre = titre,
                ContenuDuCours = contenu,
                EnseignantId = enseignantId,
            };

            cours.Etat = EtatContenuPedagogique.A_Valider;

            _bddContext.ContenusPedagogiques.Add(cours);
            _bddContext.SaveChanges();

            return cours.Id;
        }

        public int CreerContenuPedagogique(ContenuPedagogique cours)
        {
            cours.Etat = EtatContenuPedagogique.A_Valider;

            _bddContext.ContenusPedagogiques.Add(cours);
            _bddContext.SaveChanges();

            return cours.Id;
        }

        public void ModifierContenuPedagogique(ContenuPedagogique cours)
        {
            _bddContext.ContenusPedagogiques.Update(cours);
            _bddContext.SaveChanges();
        }

        //Méthode pour récupérer tous le contenu pédagogique, peu importe l'état du contenu
        public List<ContenuPedagogique> ObtenirTousLesContenusPedagogiques()
        {
            List<ContenuPedagogique> listeCours = _bddContext.ContenusPedagogiques.ToList();
            foreach (ContenuPedagogique cours in listeCours)
            {
                cours.Enseignant = _bddContext.Enseignants.Find(cours.EnseignantId);
                cours.Enseignant.Utilisateur = _bddContext.Utilisateurs.Find(cours.Enseignant.UtilisateurId);
            }

            return listeCours;
        }

        //Méthode pour récupérer le contenu pédagogique validé (c'est à dire en ligne, apparait pour l'élève/enseignant)
        public List<ContenuPedagogique> ObtenirTousLesContenusPedagogiquesValides()
        {
            List<ContenuPedagogique> listeCours = _bddContext.ContenusPedagogiques.Where(c => c.Etat == EtatContenuPedagogique.En_Ligne).ToList();
            foreach (ContenuPedagogique cours in listeCours)
            {
                    cours.Enseignant = _bddContext.Enseignants.Find(cours.EnseignantId);
                    cours.Enseignant.Utilisateur = _bddContext.Utilisateurs.Find(cours.Enseignant.UtilisateurId);
            }
            return listeCours;
        }

        public void SupprimerContenuPedagogique(int id)
        {
            ContenuPedagogique cours = _bddContext.ContenusPedagogiques.Find(id);
            _bddContext.ContenusPedagogiques.Remove(cours);
            _bddContext.SaveChanges();
        }

        public List<ContenuPedagogique> RechercherCours(TypeNiveau niveau, TypeMatiere matiere)
        {
            List<ContenuPedagogique> listeCours = _bddContext.ContenusPedagogiques.Where(c => c.Niveau == niveau && c.Matiere == matiere).ToList();
            foreach (ContenuPedagogique cours in listeCours)
            {
                cours.Enseignant = _bddContext.Enseignants.Find(cours.EnseignantId);
                cours.Enseignant.Utilisateur = _bddContext.Utilisateurs.Find(cours.Enseignant.UtilisateurId);
            }

            return listeCours;
        }
        //Méthode pour récupérer l'ensemble des cours d'un enseignant)
        public List<ContenuPedagogique> TrouverLesCours(int enseignantId)
        {
            List<ContenuPedagogique> contenuPedagogiques = _bddContext.ContenusPedagogiques.Where(r => r.EnseignantId == enseignantId).ToList();

            foreach (ContenuPedagogique contenuPedagogique in contenuPedagogiques)
            {
                contenuPedagogique.Enseignant = _bddContext.Enseignants.Find(contenuPedagogique.EnseignantId);
                contenuPedagogique.Enseignant.Utilisateur = _bddContext.Utilisateurs.Find(contenuPedagogique.Enseignant.UtilisateurId);
            }

            return contenuPedagogiques;
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public int CompterCoursSelonEtat(EtatContenuPedagogique etat)
        {
            List<ContenuPedagogique> listeCours = _bddContext.ContenusPedagogiques.ToList();
            int nbCours = 0;
            foreach (ContenuPedagogique cours in listeCours)
            {
                if (etat == cours.Etat)
                {
                    nbCours = _bddContext.ContenusPedagogiques.Where(c => c.Etat == etat).Count();
                }
            }
            return nbCours;
        }

        public int CompterTotalCours()
        {
            int nbCours = _bddContext.ContenusPedagogiques.Count();
            return nbCours;
        }
    }
}
