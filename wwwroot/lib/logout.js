const btnEl = document.getElementById("iconLogout");
const btnCC = document.getElementById("btnCancel");

let state = 0; 
function open() {
    document.getElementById("logout").style.display = "block";
    document.getElementsByClassName("post")[0].style.position = "fixed"
    // document.querySelector("body").style.backgroundColor = "pink";
    document.querySelectorAll("button:not(#btnLogout):not(#btnCancel)").forEach(element => {
        element.disabled = "true";
    });
    document.querySelectorAll("a").forEach(element=>{
     element.style.pointerEvents = "none";
    })
}

function close(){
    document.getElementById("logout").style.display = "none"
    document.querySelectorAll("button:not(#btnLogout):not(#btnCancel)").forEach(element => {
        element.disabled = "false";
    });
    document.querySelectorAll("a").forEach(element=>{
        element.style.pointerEvents = "auto";
       })
}

btnEl.addEventListener('click', open); // Pass reference to open function
btnCC.addEventListener('click', close)
