using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace Tutorin.Models
{
    public class ContenuPedagogique
    {
        public int Id { get; set; }
        
        public TypeMatiere Matiere { get; set; }
        public TypeNiveau Niveau { get; set; }
        public string Titre { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [Display(Name = "Date de publication")]
        public DateTime DatePublication { get; set; }

        [Display(Name = "Date de mise à jour")]
        [DataType(DataType.Date)]
        public DateTime DateMiseAJour { get; set; }

        public EtatContenuPedagogique Etat { get; set; }

        [Display(Name = "Contenu du cours")]
        public string ContenuDuCours { get; set; }

        public int EnseignantId { get; set; }

        public Enseignant Enseignant { get; set; }
    }
}