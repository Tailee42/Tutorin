using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class GestionnaireViewModel
    {
        public Gestionnaire Gestionnaire { get; set; }
        public List<Gestionnaire> ListeGestionnaires { get; set; }
        public NewPassword NewPassword { get; set; }

    }
}
