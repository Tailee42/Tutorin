namespace Tutorin.Models
{
    public enum EtatContenuPedagogique 
    {
        A_Valider,
        A_Modifier,
        En_Ligne
    }

    public static class EtatContenuPedagogiqueExtensions
    {
        public static string NomEtat(EtatContenuPedagogique cours)
        {
            switch ((int)cours)
            {
                case 0:
                    return "A valider";
                case 1:
                    return "A modifier";
                case 2:
                    return "En ligne";
            }
            return null;
        }


    }
}
