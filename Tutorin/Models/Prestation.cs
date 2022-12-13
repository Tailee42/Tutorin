using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tutorin.Models
{
    public class Prestation
    {
        public int Id { get; set; }
        [Required]
        public TypeNiveau Niveau { get; set; }
        [Required]
        public DateTime DateDebut { get; set; }
        [Required]
        public DateTime DateFin { get; set; }
        [Required]
        public TypePrestation TypePrestation { get; set; } 
        public string Ville { get; set; }
        public float Prix { get; set; }
        [Required]
        public TypeMatiere Matiere { get; set; }
        [Required]
        public Boolean Presentiel { get; set; }
        public EtatPrestation EtatPrestation { get; set; }
        public string LienVisio { get; set; }

        public int? EnseignantId { get; set; }
        public Enseignant Enseignant { get; set; }
        public int? PayementId { get; set; }
        public Payement Payement { get; set; }

        //public ICollection<Eleve> Eleves { get; set; }
        public List<PrestationEleve> PrestationsEleves { get; set; }
    }
}
