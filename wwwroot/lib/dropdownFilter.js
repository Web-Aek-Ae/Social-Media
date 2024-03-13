var dBtn = document.getElementsByClassName("filterBtn");
function dropdown() {
        var dF = document.getElementsByClassName("dropdownFilter");
        var i;
        for (i = 0; i < dF.length; i++) {
            var openDropdown = dF[i];
            openDropdown.classList.toggle('show');
        }
    }
dBtn[0].addEventListener('click', dropdown()); 