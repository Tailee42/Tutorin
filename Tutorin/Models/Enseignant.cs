using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorin.Models
{
    public class Enseignant
    {
        public int Id { get; set; }
        [Display(Name ="Matière")]
        public TypeMatiere Matiere { get; set; }
        [Display(Name = "Niveau")]
        public TypeNiveau Niveaux { get; set; }
        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public List<Prestation> Prestations { get; set; }
        public List<ContenuPedagogique> ContenuPedagogiques { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }

    }
}
