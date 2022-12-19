using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tutorin.Models
{
    public class Gestionnaire
    {
        public int Id { get; set; }

        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }

        [Display (Name="Poste occupé")]
        public string PosteOccupe { get; set; }

    }
}
