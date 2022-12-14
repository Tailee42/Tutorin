using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.Services
{
    public interface IPrestationServices : IDisposable
    {
        int CreerPrestation(TypeNiveau niveau, TypeMatiere matiere, DateTime dateDebut, DateTime dateFin, TypePrestation prestation, 
        string Ville, float prix, Boolean Presentiel, string lienVisio, int enseignantId );
        void ModifierPrestation(Prestation prestation);
        void SupprimerPrestation(int id);
        List<Prestation> ObtientTousLesPrestations();
        void InscrireEleveAPrestation(int eleveId, int prestationId);
        void InscrireEnseignantAPrestation(int id, int prestationId);
        List<Prestation> ObtientToutesLesPrestationsValidees();
        List<Prestation> ObtientToutesLesPrestationsCreees();
        int CompterTotalPrestations();
        int CompterPrestationSelonEtat(EtatPrestation etat);
    }
}
