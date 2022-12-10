using System;
using System.Collections.Generic;
using System.Linq;
using Tutorin.Models;

namespace Tutorin.Services
{
    public class AbonnementServices : IAbonnementServices
    {
        private BddContext _bddContext;

        public AbonnementServices()
        {
            _bddContext = new BddContext();
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public int CreerAbonnement(TypeAbonnement type, DateTime dateDebut, DateTime dateFin, float prix, int responsableEleveId, int eleveId)
        {
            Abonnement abonnement = new Abonnement() { Type = type, DateDebut = dateDebut, DateFin = dateFin, Prix = prix, ResponsableEleveId = responsableEleveId, EleveId = eleveId};
            _bddContext.Abonnements.Add(abonnement);
            _bddContext.SaveChanges();
            return abonnement.Id;
        }

        public void ModifierAbonnement(int id, TypeAbonnement type, DateTime dateDebut, DateTime dateFin, float prix, int responsableEleveId, int eleveId)
        {
            Abonnement abonnement = _bddContext.Abonnements.Find(id);

            if (abonnement != null)
            {
                abonnement.Type = type;
                abonnement.DateDebut = dateDebut;
                abonnement.DateFin = dateFin;
                abonnement.Prix = prix;
            }

            _bddContext.Update(abonnement);
            _bddContext.SaveChanges();
        }

        public void SupprimerAbonnement(int id)
        {
            Abonnement abonnement = _bddContext.Abonnements.Find(id);
            _bddContext.Abonnements.Remove(abonnement);
            _bddContext.SaveChanges();
        }

        public List<Abonnement> ObtenirTousLesAbonnements()
        {
            return _bddContext.Abonnements.ToList();
        }

        //Permet de trouver tous les abonnements en fonction d'un responsable
        public List<Abonnement> TrouverAbonnements(int responsableId)
        {
            List<Abonnement> abonnements = _bddContext.Abonnements.Where(r=>r.ResponsableEleveId== responsableId).ToList();

            foreach (Abonnement abonnement in abonnements)
            {
                abonnement.ResponsableEleve = _bddContext.ResponsablesEleves.Find(abonnement.ResponsableEleveId);
                abonnement.ResponsableEleve.Utilisateur = _bddContext.Utilisateurs.Find(abonnement.ResponsableEleve.UtilisateurId);
                abonnement.Eleve = _bddContext.Eleves.Find(abonnement.EleveId);
                if (abonnement.EleveId>0)
                {
                    abonnement.Eleve.Utilisateur = _bddContext.Utilisateurs.Find(abonnement.Eleve?.UtilisateurId);
                }
            }

            return abonnements;
        }

        public void AjouterEleve (int abonnementId, Eleve eleve)
        {
            Abonnement abonnement = _bddContext.Abonnements.Find (abonnementId);
            abonnement.EleveId = eleve.Id;
            abonnement.Eleve = eleve;

            _bddContext.Update(abonnement);
            _bddContext.SaveChanges();
        }

    }
}
