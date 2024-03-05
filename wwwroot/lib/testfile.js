let toggle = 0;
function openmembers(){
    if (toggle == 1){
        document.getElementsByClassName("pop-up")[0].style.visibility = "hidden";
        document.querySelector(".sidebar").style.filter = "none";
        document.querySelector(".group-element").style.filter = "none";
        document.querySelector(".Blog").style.filter = "none";
        toggle = 0;
    }
    else{
        document.querySelector(".pop-up").style.visibility = "visible";
        document.querySelector(".sidebar").style.filter = "blur(1.15px)";
        document.querySelector(".group-element").style.filter = "blur(1.15px)";
        document.querySelector(".Blog").style.filter = "blur(1.15px)";
        toggle = 1;
        
    }
}


