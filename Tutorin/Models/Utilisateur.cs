using System.ComponentModel.DataAnnotations;

namespace Tutorin.Models
{
    public class Utilisateur
    {
        public int Id { get; set; }

        [MaxLength(20)]
        [Required(ErrorMessage = "Le nom doit être rempli.")]
        public string Nom { get; set; }

        [MaxLength(20)]
        [Display(Name = "Prénom")]
        public string Prenom { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "L'identifiant doit être rempli.")]
        public string Identifiant { get; set; }

        [MaxLength(30)]
        [Display(Name = "Mot de passe")]
        [RegularExpression(@"^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$", ErrorMessage = "Le mot de passe doit contenir entre 6 caractères dont au moins une majuscule, une minuscule et un chiffre")]
        public string MotDePasse { get; set; }

    }
}
