using System;
using System.Collections.Generic;
using System.Linq;
using Tutorin.Models;

namespace Tutorin.Services
{
    public class PrestationServices : IPrestationServices
    {
        private BddContext _bddContext;

        public PrestationServices() 
        { 
            _bddContext = new BddContext();
        }

        public int CreerPrestation(TypeNiveau niveau, TypeMatiere matiere, DateTime dateDebut, DateTime dateFin,
            TypePrestation typePrestation, string Ville, float prix, bool presentiel, string lienVisio, int enseignantId)
        {
            Prestation prestation = new Prestation() { Niveau = niveau, Matiere = matiere, DateDebut = dateDebut, DateFin = dateFin,
                TypePrestation = typePrestation, Ville = Ville, Prix = prix, Presentiel = presentiel, LienVisio = lienVisio, EnseignantId = enseignantId };

            if (prestation.EnseignantId > 0)
            {
                prestation.EtatPrestation = EtatPrestation.Enseignants_inscrits;
            } else
            {
                prestation.EtatPrestation = EtatPrestation.A_affecter;
            }

            PrestationEleve pe = new PrestationEleve();
            pe.PrestationId = prestation.Id;
            pe.Prestation = prestation;
            
            _bddContext.PrestationsEleves.Add(pe);
            _bddContext.Prestations.Add(prestation);
            _bddContext.SaveChanges();

            return prestation.Id;
        }

        public int CreerPrestation(Prestation prestation)
        {
           
            if (prestation.EnseignantId > 0)
            {
                prestation.EtatPrestation = EtatPrestation.Enseignants_inscrits;
            }
            else
            {
                prestation.EtatPrestation = EtatPrestation.A_affecter;
            }

            PrestationEleve pe = new PrestationEleve();
            pe.PrestationId = prestation.Id;
            pe.Prestation = prestation;

            _bddContext.Prestations.Add(prestation);
            _bddContext.SaveChanges();

            return prestation.Id;
        }

        public void Dispose()
        {
            _bddContext.Dispose();
        }

        public void ModifierPrestation(Prestation prestation)
        {
            _bddContext.Prestations.Update(prestation);
            _bddContext.SaveChanges();
        }

        public List<Prestation> ObtientTousLesPrestations()
        {
            List<Prestation> listePrestations = _bddContext.Prestations.ToList();
            foreach (Prestation prestation in listePrestations)
            {
                prestation.Enseignant = _bddContext.Enseignants.Find(prestation.EnseignantId);
            }

            return listePrestations;
        }

        //Méthode pour récupérer les prestations pour lesquelles un enseignant est affecté et pas encore payées par un responsable
        public List<Prestation> ObtientToutesLesPrestationsValidees()
        {
            List<Prestation> listePrestations = _bddContext.Prestations.Where(c => c.EtatPrestation == EtatPrestation.Enseignants_inscrits).ToList();
            foreach (Prestation prestation in listePrestations)
            {
                prestation.Enseignant = _bddContext.Enseignants.Find(prestation.EnseignantId);
                prestation.Enseignant.Utilisateur = _bddContext.Utilisateurs.Find(prestation.Enseignant.UtilisateurId);
            }
            return listePrestations;
        }

        public List<Prestation> ObtientToutesLesPrestationsCreees()
        {
            List<Prestation> listePrestations = _bddContext.Prestations.Where(c => c.EtatPrestation == EtatPrestation.A_affecter).ToList();
            return listePrestations;
        }

        public void SupprimerPrestation(int id)
        {
            Prestation prestation = _bddContext.Prestations.Find(id);
            _bddContext.Prestations.Remove(prestation);
            _bddContext.SaveChanges();
        }

        public void InscrireEleveAPrestation(int eleveId, int prestationId)
        {
            PrestationEleve pe = new PrestationEleve();
            pe.EleveId = eleveId;
            pe.PrestationId = prestationId;         

            _bddContext.PrestationsEleves.Add(pe);
            _bddContext.SaveChanges();
        }

        public Prestation TrouverUnePrestation(int id)
        {
            Prestation prestation = _bddContext.Prestations.Find(id);
            if (prestation.EnseignantId != null)
            {
                prestation.Enseignant = _bddContext.Enseignants.Find(prestation.EnseignantId);
                prestation.Enseignant.Utilisateur = _bddContext.Utilisateurs.Find(prestation.Enseignant.UtilisateurId);
            }

            return prestation;
        }

        public Prestation TrouverUnePrestationNonAffectee(int id)
        {
            Prestation prestation = _bddContext.Prestations.Find(id);

            return prestation;
        }

        public void InscrireEnseignantAPrestation(int id, int prestationId)
        {
            Enseignant enseignant = _bddContext.Enseignants.Find(id);
            Prestation prestation = _bddContext.Prestations.Find(prestationId);

            prestation.EnseignantId = enseignant.Id;
            prestation.Enseignant = enseignant;
            prestation.EtatPrestation = EtatPrestation.Enseignants_inscrits;
            _bddContext.SaveChanges();
        }
        public void AjouterUnPayement(int prestationId, int payementId)
        {
            PrestationPayement pp = new PrestationPayement();
            pp.PayementId = payementId;
            pp.PrestationId = prestationId;

            Prestation prestation = _bddContext.Prestations.Find(prestationId);
            prestation.EtatPrestation = EtatPrestation.Payee_par_responsable_eleve;

            _bddContext.PrestationsPayements.Add(pp);
            _bddContext.Prestations.Update(prestation);
            _bddContext.SaveChanges();
        }

        public List<Prestation> TouverLesPrestationsDUnEleve(int eleveId)
        {
            List<PrestationEleve> listePrestationsEleve = _bddContext.PrestationsEleves.Where(c => c.EleveId == eleveId).ToList();
            List<Prestation> prestations = new List<Prestation>();

            foreach(PrestationEleve prestationEleve in listePrestationsEleve)
            {
                Prestation prestation = new Prestation();
                prestation = _bddContext.Prestations.Find(prestationEleve.PrestationId);
                prestation.Enseignant = _bddContext.Enseignants.Find(prestation.EnseignantId);
                prestation.Enseignant.Utilisateur = _bddContext.Utilisateurs.Find(prestation.Enseignant.UtilisateurId);
                prestations.Add(prestation);
            }
            return prestations;
        }
    }
}
