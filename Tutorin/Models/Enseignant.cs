using System.ComponentModel.DataAnnotations;

namespace Tutorin.Models
{
    public class Enseignant
    {
        public int Id { get; set; }
        public TypeMatiere Matiere { get; set; }
        public TypeNiveau Niveaux { get; set; }
        public int UtilisateurId { get; set; }
        public Utilisateur Utilisateur { get; set; }

    }
}
