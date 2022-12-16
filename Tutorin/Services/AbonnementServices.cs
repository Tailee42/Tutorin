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

        public int CreerAbonnement(TypeAbonnement type, int responsableEleveId, int eleveId)
        {
            Abonnement abonnement = new Abonnement(type) { ResponsableEleveId = responsableEleveId, EleveId = eleveId};

            _bddContext.Abonnements.Add(abonnement);
            _bddContext.SaveChanges();
            return abonnement.Id;
        }

        public int CreerAbonnement(int payementId, TypeAbonnement type, int responsableEleveId)
        {
            Abonnement abonnement = new Abonnement(type) { ResponsableEleveId = responsableEleveId, PayementId = payementId };

            _bddContext.Abonnements.Add(abonnement);
            _bddContext.SaveChanges();
            return abonnement.Id;
        }

        public int CreerAbonnement(Abonnement abonnement)
        {
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
            }

            _bddContext.Abonnements.Update(abonnement);
            _bddContext.SaveChanges();
        }

        public void SupprimerAbonnement(int id)
        {
            Abonnement abonnement = _bddContext.Abonnements.Find(id);
            _bddContext.Abonnements.Remove(abonnement);
            _bddContext.SaveChanges();
        }

        public void FinAbonnement(int id)
        {
            Abonnement abonnement = _bddContext.Abonnements.Find(id);
            abonnement.DateFin = DateTime.Now;
            _bddContext.Abonnements.Update(abonnement);
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

        //Permet de trouver tous les abonnements en fonction d'un eleve
        public Abonnement TrouverAbonnementEleve(int eleveId)
        {
            List<Abonnement> abonnements = _bddContext.Abonnements.Where(r => r.EleveId == eleveId).ToList();


            abonnements[0].ResponsableEleve = _bddContext.ResponsablesEleves.Find(abonnements[0].ResponsableEleveId);
            abonnements[0].ResponsableEleve.Utilisateur = _bddContext.Utilisateurs.Find(abonnements[0].ResponsableEleve.UtilisateurId);
            abonnements[0].Eleve = _bddContext.Eleves.Find(abonnements[0].EleveId);
            abonnements[0].Eleve.Utilisateur = _bddContext.Utilisateurs.Find(abonnements[0].Eleve?.UtilisateurId);
           
            return abonnements[0];
        }

        public void AjouterEleve (int abonnementId, Eleve eleve)
        {
            Abonnement abonnement = _bddContext.Abonnements.Find (abonnementId);
            abonnement.EleveId = eleve.Id;
            abonnement.Eleve = eleve;

            _bddContext.Abonnements.Update(abonnement);
            _bddContext.SaveChanges();
        }

        public void SupprimerEleve(int abonnementId)
        {
            Abonnement abonnement = _bddContext.Abonnements.Find(abonnementId);

            Eleve eleve = _bddContext.Eleves.Find(abonnement.EleveId);
            Utilisateur utlisateur = _bddContext.Utilisateurs.Find(eleve.UtilisateurId);
            _bddContext.Eleves.Remove(eleve);
            _bddContext.Utilisateurs.Remove(utlisateur);

            abonnement.EleveId = null;

            _bddContext.Abonnements.Update(abonnement);
            _bddContext.SaveChanges();
        }

    }
}
