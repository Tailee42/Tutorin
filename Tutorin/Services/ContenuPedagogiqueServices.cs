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
                EnseignantId = enseignantId
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

        public List<ContenuPedagogique> ObtenirTousLesContenusPedagogiques()
        {
            List<ContenuPedagogique> listeCours = _bddContext.ContenusPedagogiques.ToList();
            foreach (ContenuPedagogique cours in listeCours)
            {
                cours.Auteur = _bddContext.Enseignants.Find(cours.EnseignantId);
            }

            return listeCours;
        }

        public void SupprimerContenuPedagogique(int id)
        {
            ContenuPedagogique cours = _bddContext.ContenusPedagogiques.Find(id);
            _bddContext.ContenusPedagogiques.Remove(cours);
            _bddContext.SaveChanges();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }
    }
}
