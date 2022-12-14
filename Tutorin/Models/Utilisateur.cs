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
        [Required(ErrorMessage = "Le prénom doit être rempli.")]
        public string Prenom { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "L'identifiant doit être rempli.")]
        public string Identifiant { get; set; }

        [MaxLength(50)]
        [Display(Name = "Mot de passe")]
        [Required(ErrorMessage = "Le mot de passe doit être rempli.")]
        [RegularExpression(@"^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$", ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères dont au moins une majuscule, une minuscule et un chiffre")]
        public string MotDePasse { get; set; }

    }

    public class NewPassword
    {
        [MaxLength(50)]
        [Display(Name = "Mot de passe actuel")]
        [Required(ErrorMessage = "Le mot de passe doit être rempli.")]
        [RegularExpression(@"^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$", ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères dont au moins une majuscule, une minuscule et un chiffre")]
        public string OldPassword { get; set; }

        [MaxLength(50)]
        [Display(Name = "Nouveau mot de passe")]
        [Required(ErrorMessage = "Le mot de passe doit être rempli.")]
        [RegularExpression(@"^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$", ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères dont au moins une majuscule, une minuscule et un chiffre")]
        public string NouveauPassword { get; set; }

        [Display(Name = "Confirmer mot de passe")]
        [MaxLength(50)]
        [Compare("NouveauPassword", ErrorMessage = "Le mot de passe ne correspond pas au nouveau mot de passe")]
        [Required(ErrorMessage = "Le mot de passe doit être rempli.")]
        [RegularExpression(@"^((?=\S*?[A-Z])(?=\S*?[a-z])(?=\S*?[0-9]).{6,})\S$", ErrorMessage = "Le mot de passe doit contenir au moins 6 caractères dont au moins une majuscule, une minuscule et un chiffre")]
        public string ConfirmPassword { get; set; }
    }
}
