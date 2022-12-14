namespace Tutorin.Models
{
    public enum TypePrestation
    {
        Tutorat, Aide_aux_devoirs, Stage_de_revision, Cours_particulier
    }


    public static class TypePrestationExtensions
    {
        public static string NomPrestation(TypePrestation prestation)
        {
            switch ((int)prestation)
            {
                case 0:
                    return "Tutorat";
                case 1:
                    return "Aide aux devoirs";
                case 2:
                    return "Stage de révision";
                case 3:
                    return "Cours Particulier";

            }
            return null;
        }

        public static string IconHTMLPrestation(TypePrestation prestation)
        {
            switch ((int)prestation)
            {
                case 0:
                    return "<i class=\"fas fa-hands-helping\"></i>";
                case 1:
                    return "<i class=\"fas fa-life-ring\"></i>";
                case 2:
                    return "<i class=\"fas fa-clipboard-list\"></i>";
                case 3:
                    return "<i class=\"fas fa-users\"></i>";

            }
            return null;
        }

    }
}
