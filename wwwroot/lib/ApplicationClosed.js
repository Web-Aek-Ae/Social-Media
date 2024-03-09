var btnEl = document.getElementsByClassName("applications-closed");
var btnCancel = document.getElementById("btnCC");

let state = 0; 
function open() {
    document.getElementById("applicationsClosed").style.visibility = "visible";
    document.getElementsByClassName("modalBg")[0].style.visibility = "visible"
}

function close(){
    document.getElementById("applicationsClosed").style.visibility = "hidden"
    document.getElementsByClassName("modalBg")[0].style.visibility = "hidden"
}

btnEl[0].addEventListener('click', open);
btnCancel.addEventListener('click', close);