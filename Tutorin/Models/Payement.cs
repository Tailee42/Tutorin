using System;
using System.ComponentModel.DataAnnotations;

namespace Tutorin.Models
{
    public class Payement
    {
        public int Id { get; set; }

        [Display(Name = "Nom du titulaire de la carte")]
        [Required, MaxLength(50)]
        public string NomTitulaireCarte { get; set; }

        [Display(Name = "Numéro de la carte")]
        [RegularExpression(@"^(\d{16})", ErrorMessage = "Il faut 16 chiffres")]
        [Required, MaxLength(16)]
        public string NumeroCarte { get; set; }

        [Display(Name = "Date d'expiration de la carte")]
        [Required, MaxLength(5)]
        [RegularExpression(@"^(\d{2})\/(\d{2})", ErrorMessage = "La date est au format mm/aa")]
        public string DateExpiration { get; set; }

        [Required, MaxLength(3)]
        [RegularExpression(@"^(\d{3})", ErrorMessage = "Il faut 3 chiffres")]
        public string CVC { get; set; }
        public DateTime DatePayement { get; set; }
        public float MontantTTC { get; set; }
        public int ResponsableEleveId { get; set; }
        public ResponsableEleve ResponsableEleve { get; set; }

        public Payement()
        {
            DatePayement = DateTime.Now;
        }
    }
}
