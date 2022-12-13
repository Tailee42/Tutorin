using System.Collections.Generic;

namespace Tutorin.Models
{
    public class Gestionnaire
    {
        public int Id { get; set; }

        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }

        public string PosteOccupe { get; set; }

    }
}
