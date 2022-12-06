using System.Collections.Generic;

namespace Tutorin.Models
{
    public class ResponsableEleve
    {
        public int Id { get; set; }
        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public List<Abonnement> Abonnements { get; set; }
    }
}
