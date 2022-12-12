using System.Collections.Generic;
using Tutorin.Models;

namespace Tutorin.ViewModels
{
    public class TableauBordEleveViewModel
    {
        public Eleve Eleve { get; set; }
        public List<ContenuPedagogique> CoursSuivis { get; set; }
    }
}
