using System.ComponentModel;

namespace Tutorin.Models
{
    public enum TypeNiveau
    {
        CP, 
        CE1, 
        CE2, 
        CM1, 
        CM2,
        Sixieme, 
        Cinquieme, 
        Quatrieme, 
        Troisieme, 
        Seconde, 
        Premiere, Terminale
    }

    public static class TypeNiveauExtensions
    {
        public static string NomDuNiveau(TypeNiveau niveau)
        {
            switch ((int)niveau)
            {
                case 0:
                    return "CP";
                case 1:
                    return "CE1";
                case 2:
                    return "CE2";
                case 3:
                    return "CM1";
                case 4:
                    return "CM2";
                case 5:
                    return "Sixième";
                case 6:
                    return "Cinquième";
                case 7:
                    return "Quatrième";
                case 8:
                    return "Troisième";
                case 9:
                    return "Seconde;";
                case 10:
                    return "Première";
                case 11:
                    return "Terminale";
            }
            return null;
        }
    }
}
