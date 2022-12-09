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

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DatePublication { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateMiseAJour { get; set; }

        public EtatContenuPedagogique Etat { get; set; }
        public string ContenuDuCours { get; set; }

        public int EnseignantId { get; set; }

        public Enseignant Enseignant { get; set; }
    }
}