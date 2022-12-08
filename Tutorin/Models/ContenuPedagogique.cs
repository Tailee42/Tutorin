using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Tutorin.Models
{
    public class ContenuPedagogique
    {
        public int Id { get; set; }
        
        public TypeMatiere Matiere { get; set; }
        public TypeNiveau Niveau { get; set; }
        public string Titre { get; set; }
        public DateTime DatePublication { get; set; }
        public DateTime DateMiseAJour { get; set; }
        public EtatContenuPedagogique Etat { get; set; }
        public string ContenuDuCours { get; set; }

        public int EnseignantId { get; set; }
        public Enseignant Auteur { get; set; }
    }
}