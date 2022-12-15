let contenu = document.querySelector("p");
let iconeCouleur = document.getElementById("couleur");

iconeCouleur.addEventListener('click', function () {
    _formatElement(contenu, "sd");
    iconeCouleur.classList.toggle('fa-fade');
});

let iconeInterligne = document.getElementById("interligne");
let indexInterligne = 0;
iconeInterligne.addEventListener('click', function () {
    if (indexInterligne == 0) {
        if (indexSize == 1) {
            contenu.classList.add('interligneGrand');
        }
        indexInterligne = 1;
    } else {
        if (indexSize == 1) {
            contenu.classList.remove('interligneGrand');
        }
        indexInterligne = 0;
    }
    iconeInterligne.classList.toggle('fa-fade');
    contenu.classList.toggle('interligne');
});

let iconePolice = document.getElementById("police");

iconePolice.addEventListener('click', function () {
    iconePolice.classList.toggle('fa-fade');
    contenu.classList.toggle('police');
});


let iconeSize = document.getElementById("size");
let indexSize = 0;
iconeSize.addEventListener('click', function () {
    if (indexSize == 0) {
        if (indexInterligne == 1) {
            contenu.classList.add('interligneGrand');
        }
        indexSize = 1;
    } else {
        if (indexInterligne == 1) {
            contenu.classList.remove('interligneGrand');
        }
        indexSize = 0;
    }
    iconeSize.classList.toggle('fa-fade');
    contenu.classList.toggle('size');
});



function _formatElement( elt, mode ) {
  console.log("function format element");
  // traitement des noeuds enfants
  for (var i=0; i<elt.childNodes.length; i+=1) {
    // traitement du noeud texte
    if ((elt.childNodes[i].nodeType == 3) || (elt.childNodes[i].nodeType == 4) || (elt.childNodes[i].nodeName == 'SPAN')) { // Text ou CDATA
      paragraphe = elt.childNodes[i].textContent;
      var pos = 0;
      var pmots = paragraphe.match(/([a-z@àäâéèêëîïôöûùçœ'’]+)/gi);
      if (pmots !== null) {
        var para = document.createElement("span");
        para.className = "lirecouleur";

        // remplace le texte d'origine par le texte traité
        elt.replaceChild(para, elt.childNodes[i]);

        pmots.forEach(function( element, index, array ) {
          var i = paragraphe.indexOf( element, pos );
          para.appendChild( document.createTextNode( paragraphe.substring(pos, i) ) );

          var phon = LireCouleur.extrairePhonemes( element );

          if ( mode.startsWith( 's' ) ) {
            var sylls = LireCouleur.extraireSyllabes( phon );
            LireCouleurFormateur.formatSyllabes( document, para, sylls, mode );
          }
          else {
            LireCouleurFormateur.formatPhonemes( document, para, phon );
          }

          pos += element.length+( i-pos );
        });
        para.appendChild( document.createTextNode( paragraphe.substring(pos) ) );
      }
    }
    else {
      _formatElement( elt.childNodes[i], mode );
    }
  }
}
