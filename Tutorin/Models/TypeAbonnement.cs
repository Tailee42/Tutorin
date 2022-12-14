namespace Tutorin.Models
{
    public enum TypeAbonnement
    {
        CoursEnLigne,
        Tutorat
    }

    public static class TypeAbonnementExtensions
    {
        public static string NomAbonnement(TypeAbonnement abonnement)
        {
            switch ((int)abonnement)
            {
                case 0:
                    return "Cours en ligne";
                case 1:
                    return "Tutorat";

            }
            return null;
        }

        public static string NomAbonnement(int abonnement)
        {
            switch (abonnement)
            {
                case 0:
                    return "Cours en ligne";
                case 1:
                    return "Tutorat";

            }
            return null;
        }

        public static float PrixTTCAbonnement(TypeAbonnement abonnement)
        {
            switch ((int)abonnement)
            {
                case 0:
                    return 6.99F;
                case 1:
                    return 16.99F;

            }
            return 0;
        }
    }
    
}
