
document.getElementById("navbar-mobile-list").innerHTML = document.getElementById("navbar-desktop").innerHTML;

/* Commande du burgerMenu*/
const icone = document.querySelector('.navbar-mobile i');
console.log(icone);

const modal = document.querySelector('.modal');
console.log(modal)

icone.addEventListener('click', function () {
    console.log("icone cliqué");
    modal.classList.toggle('change-modal');
    icone.classList.toggle('fa-times');
});
