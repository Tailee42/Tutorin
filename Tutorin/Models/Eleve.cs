using System;
using System.ComponentModel.DataAnnotations;

namespace Tutorin.Models
{
    public class Eleve
    {
        public int Id { get; set; }

        [Required]
        public DateTime DateNaissance { get; set; }

        [Required]
        public TypeNiveau Niveau { get; set; }

        public int UtilisateurID { get; set; }
        public Utilisateur Utilisateur { get; set; }
    }
}
