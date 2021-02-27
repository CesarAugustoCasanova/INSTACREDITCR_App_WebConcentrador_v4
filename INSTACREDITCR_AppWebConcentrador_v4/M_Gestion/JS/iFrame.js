//https://javascript-compressor.com/
//para comprimir este archivo

//Cierra la pagina si no son iframes

if (inIframe())
    window.location.href = "MasterPage.aspx";

function inIframe() { try { return window.self == window.top; } catch (e) { return true; } }