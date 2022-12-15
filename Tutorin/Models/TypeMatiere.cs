﻿namespace Tutorin.Models
{
    public enum TypeMatiere
    {
       Anglais, Français, Mathematiques, Physique, Chimie, Histoire, Geographie
    }


    public static class TypeMatiereExtensions
    {
        public static string IconHTMLPrestation(TypeMatiere matiere)
        {
            switch ((int)matiere)
            {
                case 0:
                    return "<i class=\"fa-solid fa-language\"></i>";
                case 1:
                    return "<i class=\"fa-solid fa-book-bookmark\"></i>";
                case 2:
                    return "<i class=\"fa-solid fa-square-root-variable\"></i>";
                case 3:
                    return "<i class=\"fa-solid fa-gears\"></i>";
                case 4:
                    return "<i class=\"fa-solid fa-flask-vial\"></i>";
                case 5:
                    return "<i class=\"fa-solid fa-landmark\"></i>";
                case 6:
                    return "<i class=\"fa-solid fa-earth-europe\"></i>";

            }
            return null;
        }
    }
}
