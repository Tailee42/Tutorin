/*
 *
 */
function LireCouleurStyle(chaine) {
  var moi = this;
  this.dict = {};
  var chainespl = chaine.split(';');
  for(var un_item in chainespl) {
    if (un_item.length > 0) {
      var attval = un_item.split(':');
      if (attval.length == 2) {
        moi.dict[attval[0].trim()] = attval[1].trim();
      }
    }
  }
}

/*
 * Transforme un style en chaîne de caractères
 */
LireCouleurStyle.prototype.toString = function() {
  var stl = "";
  for(var key in this.dict) {
    stl += key + ': ' + this.dict[key] + '; ';
  }
  return stl;
}

/*
 *
 */
function LireCouleurFormat() {
  this.correspondances = {'verb_3p':'phon_muet', '#':'phon_muet', 'q_caduc':'phon_e',
    'a':'phon_a', 'q':'phon_e', 'i':'phon_i', 'o':'phon_o', 'o_comp':'phon_o',
    'o_ouvert':'phon_o', 'u':'phon_ou', 'y':'phon_u', 'e':'phon_ez',
    'e_comp':'phon_ez', 'w':'phon_w', 'wa':'phon_wa', 'w5':'phon_w5',
    'e^':'phon_et', 'e^_comp':'phon_et', 'a~':'phon_an', 'e~':'phon_in',
    'x~':'phon_un', 'o~':'phon_on', 'x':'phon_e', 'x^':'phon_eu', 'j':'phon_y',
    'z_s':'phon_z', 'g_u':'phon_g', 'z^_g':'phon_ge', 's_x':'phon_s',
    'n~':'phon_gn', 'g~':'phon_ng', 'p':'phon_p', 't':'phon_t', 'k':'phon_k', 'k_qu':'phon_k',
    'b':'phon_b', 'd':'phon_d', 'g':'phon_g', 'f':'phon_f', 'f_ph':'phon_f',
    's':'phon_s', 's_c':'phon_s', 's_t':'phon_s', 's^':'phon_ch', 'v':'phon_v',
    'z':'phon_z', 'z^':'phon_ge', 'm':'phon_m', 'n':'phon_n', 'l':'phon_l',
    'r':'phon_r', 'ks':'phon_ks', 'gz':'phon_gz'};

  /*
   * Code les typographies adoptées pour les phonèmes et les syllabes
   */
  this.couleurs = {
    "syl_1" : "color: #0000ff;",
    "syl_2" : "color: #ff0000;",
    "syl_3" : "color: #008000;",
    "muet" : "color: #cccccc;",
    "phon_muet": "color: #aaaaaa;",
    "phon_e": "color: #ff0000;",
    "phon_a": "color: #0023ff;",
    "phon_i": "color: #a9d82e;",
    "phon_o": "color: #cf6633;",
    "phon_ou": "color: #ffcc00; ",
    "phon_u": "color: #008000;",
    "phon_ez": "color: #00dbc5;",
    "phon_w": "color: #892ca0;",
    "phon_wa": "color: #892ca0;",
    "phon_w5": "color: #3deb3d;",
    "phon_et": "color: #666699;",
    "phon_an": "color: #0023ff; font-family: serif; font-style: italic; font-weight: bold;",
    "phon_in": "color: #a9d82e; font-family: serif; font-style: italic; font-weight: bold;",
    "phon_un": "color: #a9d82e; font-family: serif; font-style: italic; font-weight: bold;",
    "phon_on": "color: #cf6633; font-family: serif; font-style: italic; font-weight: bold;",
    "phon_eu": "color: #198a8a;",
    "phon_y": "color: #000; font-family: serif; font-style: italic; text-decoration: underline;",
    "phon_z": "color: #31859b; font-weight: bold; font-style: italic; ",
    "phon_g": "color: #632423; ",
    "phon_ge": "color: #205867; ",
    "phon_s": "color: #5f497a; font-weight: bold; font-style: italic; ",
    "phon_gn": "color: #938953; font-weight: bold; font-style: italic; font-family: serif; ",
    "phon_ng": "color: #494429; ",
    "phon_p": "color: #c3d69b; font-weight: bold; font-style: italic; font-family: serif; ",
    "phon_t": "color: #76923c; font-weight: bold; font-style: italic; ",
    "phon_k": "color: #4f6128; ",
    "phon_b": "color: #d99694; font-weight: bold; font-style: italic; font-family: serif; ",
    "phon_d": "color: #953734; font-weight: bold; font-style: italic; ",
    "phon_f": "color: #b2a2c7; font-weight: bold; font-family: serif; font-style: italic; ",
    "phon_ch": "color: #3f3151; ",
    "phon_v": "color: #92cddc; font-weight: bold; font-style: italic; font-family: serif; ",
    "phon_m": "color: #e36c09; font-weight: bold; font-style: italic; ",
    "phon_n": "color: #974806; ",
    "phon_l": "color: #fac08f; font-weight: bold; font-style: italic; font-family: serif; ",
    "phon_r": "color: #974806; font-family: serif; ",
    "phon_ks": "color: #548dd4; font-weight: bold; font-style: italic; font-family: serif; ",
    "phon_gz": "color: #0f243e; "
  };

  this.style_semi = {
    "w" : "	font-family: serif; font-style: italic; border-style: none none dotted none;",
    "y" : "	font-family: serif; font-style: italic; border-style: none none double none;",
    "j" : "	font-family: serif; font-style: italic; text-decoration: underline;",
  };

  // compteur de syllabes (alternance de couleurs)
  this._isyl = 0;
  // traitement appliqué (0=noir ; 1=phonèmes ; 2=syllabes ; 3=lettres muettes)
  this._traitement = 0;

  this.phonemes = {};
  for(var key in this.correspondances) {
    this.phonemes[this.correspondances[key]] = false;
  }
}
var LireCouleurFormateur = new LireCouleurFormat();

/*
 * Convertit les données en texte
 */
LireCouleurFormat.prototype.toString = function() {
  var txt = '{ "lirecouleur" : [';
  for(var key in this.phonemes) {
    txt += '\n\t{"phon" : "' + key + '", "style": "'+this.couleurs[key]+'", ';
    txt += '"select": "'+this.phonemes[key].toString()+'"},';
  }

  return txt.substring(0,txt.length-1)+"\n]}";
}

/*
 * Décodage d'un mot sous la forme d'une suite de phonèmes
 */
LireCouleurFormat.prototype.setStyle = function(e, style) {
  var stl = style.split(';');
  for(var i in stl) {
    if (stl[i].length > 0) {
      var attval = stl[i].split(':');
      if (attval.length == 2) {
        e.style.setProperty(attval[0].trim(), attval[1].trim());
      }
    }
  }
}

/*
 * Décodage d'un mot sous la forme d'une suite de phonèmes
 */
LireCouleurFormat.prototype.formatPhonemes = function(document, docfrag, l_phonemes) {
  var moi = this;
  var iphon;
  l_phonemes.forEach(function(element, index, array) {
    var e = document.createElement("span");

    if (element.estSemiConsonne()) {
      var il = 0;
      iphon = moi.correspondances[element.phoneme.substring(2)];
      if (element.estPhoneme() && moi.phonemes[iphon]) {
        moi.setStyle(e, moi.couleurs[iphon]);

        il = 1;
        if (element.phoneme.startsWith('w_') && element.lettres.startsWith('ou')) {
          // micmac pour savoir s'il faut souligner une ou 2 lettres
          il = 2;
        }

        var ee = document.createElement("span");
        ee.style = moi.style_semi[element.phoneme.substring(0, 1)];
        ee.appendChild(document.createTextNode(element.lettres.substring(0, il)));
        e.appendChild(ee);
      }

      e.appendChild(document.createTextNode(element.lettres.substring(il)));
    }
    else {
      iphon = moi.correspondances[element.phoneme];
      if (element.estPhoneme() && moi.phonemes[iphon]) {
        moi.setStyle(e, moi.couleurs[iphon]);
      }
      e.appendChild(document.createTextNode(element.lettres));
    }
    docfrag.appendChild(e);
  });
  return docfrag;
}

/*
 * Décodage d'un mot sous la forme d'une suite de phonèmes
 */
LireCouleurFormat.prototype.formatSyllabes = function(document, docfrag, l_syllabes, mode) {
  var moi = this;
  var muet = moi.couleurs['phon_muet'];
  l_syllabes.forEach(function(element, index, array) {
    var e = document.createElement("span");
    if ( mode == "sd" ) {
      //e.style = moi.couleurs['syl_'+(moi._isyl+1).toString()];
      moi.setStyle(e, moi.couleurs['syl_'+(moi._isyl+1).toString()]);
    }
    e.style.verticalAlign = "top";

    var esyl = element.texte(e, muet);
    docfrag.appendChild(esyl);
    moi._isyl = ((moi._isyl+1) % 2); //Changer le 2 pour changer le nombre de couleurs

    if ( mode == "s" ) {
      // ajout d'un arc sous la syllabe
      var width = esyl.getBoundingClientRect().width;

      var arc = document.createElement("img");
      arc.width = width;
      arc.height = 12;
      arc.src = "img/syllb.png";
      e.style.width = width.toString()+"px";
      e.style.display = "inline-block";
      e.style.lineHeight = "1";
      e.appendChild(arc);
    }
  });
  return docfrag;
}




