var dBtn = document.getElementsByClassName("filterBtn");
function dropdown() {
    console.log('a');
    var dropdowns = document.getElementsByClassName("dropdownFilter");
    var i;
    for (i = 0; i < dropdowns.length; i++) {
        var openDropdown = dropdowns[i];
        openDropdown.classList.toggle('showFilter');
    }
}

window.onclick = function(event) {
    if (!event.target.matches('.filterBtn')) {
      var dropdowns = document.getElementsByClassName("dropdownFilter");
      var i;
      for (i = 0; i < dropdowns.length; i++) {
      //   console.log(dropdowns[i])
        var openDropdown = dropdowns[i];
        if (openDropdown.classList.contains('showFilter')) {console.log('b');
          openDropdown.classList.remove('showFilter');
        }
      }
    }
  }
dBtn[0].addEventListener('click', dropdown); 