using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class ResponsableEleveViewModel
    {
       public ResponsableEleve ResponsableEleve { get; set; }
       public List<ResponsableEleve> ListeResponsablesEleves { get; set; }
       public NewPassword NewPassword { get; set; }
    }
}
