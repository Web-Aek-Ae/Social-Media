var btnEl = document.getElementById("delete-post");
var btnCancel = document.getElementById("btnCancel");
var btnDelete = document.getElementById("btnDelete");

let state = 0; 
function open() {
    document.getElementById("delete").style.visibility = "visible";
    document.getElementsByClassName("modalBg")[0].style.visibility = "visible"
}

function close(){
    document.getElementById("delete").style.visibility = "hidden"
    document.getElementsByClassName("modalBg")[0].style.visibility = "hidden"
}
console.log("a")
btnEl.addEventListener('click', function(){ alert("Hello World!");});
btnCancel.addEventListener('click', close);