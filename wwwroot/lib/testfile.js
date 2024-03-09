let toggle = 0;
function openmembers(){
    if (toggle == 1){
        document.getElementsByClassName("pop-up")[0].style.visibility = "hidden";
        document.getElementsByClassName("popup-backdrop")[0].style.visibility = "hidden";
        toggle = 0;
    }
    else{
        document.querySelector(".pop-up").style.visibility = "visible";
        document.querySelector(".popup-backdrop").style.visibility = "visible"; 
        toggle = 1;
    }
}


