using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tutorin.Models
{
    public class Eleve
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [Required]
        public DateTime DateNaissance { get; set; }

        [Required]
        public TypeNiveau Niveau { get; set; }

        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }

        //public ICollection<Prestation> Prestations { get; set; }
        public List<PrestationEleve> PrestationsEleves { get; set;}
        public List<Prestation> Prestations { get; set;}
    }
}
