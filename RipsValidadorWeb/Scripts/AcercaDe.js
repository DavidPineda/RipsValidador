function iniciar() {
    var titulo = document.getElementById("acercaDe");
    titulo.addEventListener('click', mostrarVersion, false);
}

function mostrarVersion() {
    var ancho = (window.innerWidth / 2) - 285;
    var altura = (window.innerHeight / 2) - 215;
    window.open("../Account/Version.html", "Acerca de Integra Rips", "Width=570,Height=430,Resizable='NO',Scrollbars=NO,Titlebar=NO,Location=NO,toolbar=NO,Top=" + altura + ",Left=" + ancho + "");
}

window.addEventListener('load', iniciar, false);