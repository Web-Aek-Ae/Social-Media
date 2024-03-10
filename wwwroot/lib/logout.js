const btnEl = document.getElementById("iconLogout");
const btnCC = document.getElementById("btnCancel");
const btnLO = document.getElementById("btnLogout");

let state = 0; 
function openLogout() {
    document.getElementById("logout").style.visibility = "visible";
    document.getElementsByClassName("modalBg")[0].style.visibility = "visible"
}

function closeLogout(){
    document.getElementById("logout").style.visibility = "hidden"
    document.getElementsByClassName("modalBg")[0].style.visibility = "hidden"
}

btnEl.addEventListener('click', openLogout);
btnCC.addEventListener('click', closeLogout)
