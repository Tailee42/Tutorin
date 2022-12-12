namespace Tutorin.Models
{
    public class PrestationEleve
    {
        //table intermédiaire liant les prestations aux éleves (relation pls à pls)
        //création de liste dans les tables prestations et éleves
        public int Id { get; set; }

        public int EleveId { get; set; }
        public Eleve Eleve { get; set; }

        public int PrestationId { get; set; }
        public Prestation Prestation { get; set; }
    }
}
