using System;
using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class EleveViewModel
    {
        public Eleve Eleve { get; set; }
        public List<Eleve> ListeEleves { get; set; }
        public NewPassword NewPassword { get; set; }
    }
}
