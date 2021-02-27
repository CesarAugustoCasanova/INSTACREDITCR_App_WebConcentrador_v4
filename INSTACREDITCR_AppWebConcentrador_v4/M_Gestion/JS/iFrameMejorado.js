if (inIframe()) window.location.href = "MasterPage.aspx";
function inIframe(){try{return window.self == window.top;}catch (e) {return true;}}