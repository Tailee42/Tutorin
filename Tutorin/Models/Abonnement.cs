using System;

namespace Tutorin.Models
{
    public class Abonnement
    {
        public int Id { get; set; }
        public TypeAbonnement Type { get; set; }
        public DateTime DateDebut { get; set; }
        public DateTime DateFin { get; set; }
        public float Prix { get; set; }
        public const float TVA = 0.2F; 

        public int ResponsableEleveId { get; set; }
        public ResponsableEleve ResponsableEleve { get; set; }

        public int EleveId { get; set; }
        //public Eleve Eleve { get; set; }
    }
}

