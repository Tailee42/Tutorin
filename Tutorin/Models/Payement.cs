using System;

namespace Tutorin.Models
{
    public class Payement
    {
        public int Id { get; set; }
        public string NomTitulaireCarte { get; set; }
        public long NumeroCarte { get; set; }
        public int DateExpiration { get; set; }
        public int CVC { get; set; }
        public DateTime DatePayement { get; set; }
        public float MontantTTC { get; set; }
        public int ResponsableEleveId { get; set; }
        public ResponsableEleve ResponsableEleve { get; set; }

        public Payement()
        {
            DatePayement = DateTime.Now;
        }
    }
}
