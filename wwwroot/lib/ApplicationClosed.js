const btnEl = document.getElementById("applications-closed");
const btnCancle = document.getElementById("btnCancel");
const btnClosed = document.getElementById("btnClosed");

let state = 0; 
function open() {
    document.getElementById("applicationsClosed").style.visibility = "visible";
    document.getElementsByClassName("modalBg")[0].style.visibility = "visible"
}

function close(){
    document.getElementById("applicationsClosed").style.visibility = "hidden"
    document.getElementsByClassName("modalBg")[0].style.visibility = "hidden"
}

btnEl.addEventListener('click', open);
btnCancle.addEventListener('click', close)
