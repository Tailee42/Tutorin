namespace Tutorin.Models
{
    public enum TypeMatiere
    {
       Anglais, Français, Mathematiques, Physique, Chimie, Histoire, Geographie
    }

    public static class TypeMatiereExtensions
    {
        public static string NomMatiere(TypeMatiere matiere)
        {
            switch ((int)matiere)
            {
                case 0:
                    return "Anglais";
                case 1:
                    return "Français";
                case 2:
                    return "Mathématiques";
                case 3:
                    return "Physique";
                case 4:
                    return "Chimie";
                case 5:
                    return "Histoire";
                case 6:
                    return "Géographie";
            }
            return null;
        }


    }
}
