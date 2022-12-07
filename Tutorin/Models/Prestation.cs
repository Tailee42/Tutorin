using System;

namespace Tutorin.Models
{
    public class Prestation
    {
        public int Id { get; set; }
        public TypeNiveau Niveau { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public TypePrestation TypePrestation { get; set; } 
        public string Ville { get; set; }
        public double Prix { get; set; }
        public TypeMatiere TypeMatiere { get; set; }
        public Boolean Presentiel { get; set; }
        public EtatPrestation EtatPrestation { get; set; }
        public string LienVisio { get; set; }
        public int EleveId { get; set; }
        public Eleve Eleve { get; set; }
        public int EnseignantId { get; set; }
        public Enseignant Enseignant { get; set; }

    }
}
