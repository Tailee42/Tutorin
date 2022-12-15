/*

*/
function mouseOverColor(hex) {
  document.body.style.cursor = "pointer";
}

function mouseOutMap() {
  document.body.style.cursor = "";
}

function clickColor(colorhex, seltop, selleft) {
  var colormap, areas, cc, i, areacolor, cc;
  if ((!seltop || seltop == -1) && (!selleft || selleft == -1)) {
    colormap = document.getElementById("colormap");
    areas = colormap.getElementsByTagName("AREA");
    for (i = 0; i < areas.length; i++) {
      areacolor = areas[i].getAttribute("onmouseover").replace('mouseOverColor("', '');
      areacolor = areacolor.replace('")', '');
      if (areacolor.toLowerCase() == colorhex) {
        cc = areas[i].getAttribute("onclick").replace(')', '').split(",");
        seltop = Number(cc[1]);
        selleft = Number(cc[2]);
      }
    }
  }
  if ((seltop+200)>-1 && selleft>-1) {
    document.getElementById("selectedhexagon").style.top=seltop + "px";
    document.getElementById("selectedhexagon").style.left=selleft + "px";
    document.getElementById("selectedhexagon").style.visibility="visible";

    var config = CKEDITOR.config.lirecouleur;
    if (config['phonid'].length > 0) {
      config['color'] = colorhex;
      LireCouleurFormateur.couleurs[config['phonid']] = assembleStyle( config );
      document.getElementById( config['apercu'] ).style.color = colorhex;
    }
  } else {
    document.getElementById("selectedhexagon").style.visibility = "hidden";
  }
}

/*

*/
function assembleStyle( stl )
{
  var arr = new Array();
  for (var key in stl) {
    if (( stl[key].length > 0 ) && ( key != 'phonid' ) && ( key != 'apercu' )) {
      arr.push(key+': '+stl[key]);
    }
  }
  var res = arr.join("; ")+';';
  delete arr;
  return res;
}

