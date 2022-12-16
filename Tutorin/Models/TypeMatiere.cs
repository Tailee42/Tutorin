namespace Tutorin.Models
{
    public enum TypeMatiere
    {
       Choix_Matiere, Anglais, Français, Mathematiques, Physique, Chimie, Histoire, Geographie
    }

    public static class TypeMatiereExtensions
    {
        public static string NomMatiere(TypeMatiere matiere)
        {
            switch ((int)matiere)
            {
                case 0:
                    return "Choisir une matière";
                case 1:
                    return "Anglais";
                case 2:
                    return "Français";
                case 3:
                    return "Mathématiques";
                case 4:
                    return "Physique";
                case 5:
                    return "Chimie";
                case 6:
                    return "Histoire";
                case 7:
                    return "Géographie";
            }
            return null;
        }

        public static string IconHTMLPrestation(TypeMatiere matiere)
        {
            switch ((int)matiere)
            {
                case 1:
                    return "<i class=\"fa-solid fa-language\"></i>";
                case 2:
                    return "<i class=\"fa-solid fa-book-bookmark\"></i>";
                case 3:
                    return "<i class=\"fa-solid fa-square-root-variable\"></i>";
                case 4:
                    return "<i class=\"fa-solid fa-gears\"></i>";
                case 5:
                    return "<i class=\"fa-solid fa-flask-vial\"></i>";
                case 6:
                    return "<i class=\"fa-solid fa-landmark\"></i>";
                case 7:
                    return "<i class=\"fa-solid fa-earth-europe\"></i>";

            }
            return null;
        }
    }
}
