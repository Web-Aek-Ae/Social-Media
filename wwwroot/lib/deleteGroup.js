var btnDelete = document.getElementsByClassName("delete-group");
var btnCancel = document.getElementById("btnCancle");

function open() {
    document.getElementById("delete").style.visibility = "visible";
    document.getElementsByClassName("popup-backdrop")[0].style.visibility = "visible"
}
function close(){
    document.getElementById("delete").style.visibility = "hidden"
    document.getElementsByClassName("popup-backdrop")[0].style.visibility = "hidden"
}

btnDelete[0].addEventListener('click', open);
btnCancel.addEventListener('click', close);