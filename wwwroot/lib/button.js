const btn = document.getElementById('btn');
btn.addEventListener('click', function handleClick() {
  const initialText = 'join activity';

  if (btn.textContent.toLowerCase().includes(initialText.toLowerCase())) {
    btn.textContent = 'joined';
    btn.style.backgroundColor = "white";
    btn.style.border = "0.15rem solid black";
    btn.style.color = "black";
  } else {
    btn.textContent = initialText;
    btn.style.backgroundColor = "#fbca63";
    btn.style.border = "0";
  }
});

btn.addEventListener('mouseover',() =>{
    if (btn.textContent === 'join activity' ){
        btn.style.backgroundColor = "#f77c7e";
        btn.style.color = "#ffff";
        btn.style.transition = "0.3s";
}});

btn.addEventListener('mouseout',() =>{
    if (btn.textContent === 'join activity' ){
        btn.style.backgroundColor = "#fbca63";
        btn.style.color = "black";
        btn.style.border = "0";
        btn.style.transition = "0.3s";
}});
