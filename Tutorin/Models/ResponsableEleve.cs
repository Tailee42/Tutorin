using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tutorin.Models
{
    public class ResponsableEleve
    {
        public int Id { get; set; }

        [Required]
        public string Mail { get; set; }

        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }

        public List<Abonnement> Abonnements { get; set; }
    }
}
