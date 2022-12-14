namespace Tutorin.Models
{
    public class PrestationPayement
    {
        //table intermédiaire liant les prestations aux payements (relation pls à pls)
        //création de liste dans les tables prestation et payement
        public int Id { get; set; }

        public int PayementId { get; set; }
        public Payement Payement { get; set; }

        public int PrestationId { get; set; }
        public Prestation Prestation { get; set; }
    }
}
