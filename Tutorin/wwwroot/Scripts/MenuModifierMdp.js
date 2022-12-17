let btn = document.getElementById("btn-modifier");
let form = document.getElementById("form-modifier");

btn.addEventListener("click", () => {
    if (getComputedStyle(form).display == "none") {
        form.style.display = "block";
    } else {
        form.style.display = "none";
    }
});
