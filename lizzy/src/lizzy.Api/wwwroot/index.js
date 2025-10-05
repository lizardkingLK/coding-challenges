if (location.search.includes("error=access_denied")) {
    const divBrand = document.querySelector("#divBrand");
    divBrand.classList.remove("bg-accent");
    divBrand.classList.add("bg-danger");

    const paraDescription = document.querySelector("#paraDescription");
    paraDescription.innerHTML = "You are not authorized!?";
}