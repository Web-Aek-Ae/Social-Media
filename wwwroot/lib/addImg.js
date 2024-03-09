const btnUp = document.getElementById("Upload");
const btnCC = document.getElementById("Cancel");
const btnShow = document.getElementById("Edit");

let state = 0; 
function openLogout() {
    document.getElementById("modalImgBg").style.visibility = "visible";
    document.getElementById("modalImg").style.visibility = "visible"
}

function closeLogout(){
    document.getElementById("modalImgBg").style.visibility = "hidden"
    document.getElementById("modalImg").style.visibility = "hidden"
}

btnUp.addEventListener('click', closeLogout);
btnCC.addEventListener('click', closeLogout);
btnShow.addEventListener('click', openLogout);
