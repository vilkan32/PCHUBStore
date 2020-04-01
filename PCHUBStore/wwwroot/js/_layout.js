function loadPicturesHeader() {


    let regex = /option\d/;

    fetch("/api/layout/header").then(res => res.json()).then(x => {
        let obj = JSON.parse(x);
        let logoInfo = obj["Pictures"].find(x => x.Name === "PCHUB-Logo");
        let lightningInfo = obj["Pictures"].find(x => x.Name === "Lightning");
        document.getElementById('lightning').src = lightningInfo.Url;
        document.getElementById('logo').src = logoInfo.Url;

        let sliderOptionPictures = obj["Pictures"].filter(x => regex.exec(x.Name) && x.IsDeleted === false);

        var info = setInterval(function () {

            document.getElementById('slideShowPicture').src = sliderOptionPictures[randomInteger(0, 3)].Url;

        }, 5000);

        window.info = info;

        Array.from(document.getElementById('slideShow').children).forEach(x => x.addEventListener('change', (ev) => changePicture(ev, obj)));

        let shoppingCart = obj["Pictures"].find(x => x.Name === "shoppingCart");

        document.getElementById("shoppingCart").src = shoppingCart.Url;

    });


}


function randomInteger(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}


function changePicture(x, obj) {
    let picture = document.getElementById('slideShowPicture');
    let target = x.target;
    clearInterval(window.info);
    let requiredUrl = obj["Pictures"].find(x => x.Name === target.value).Url;
    picture.src = requiredUrl;
}





function navFunction() {

    window.addEventListener("scroll", (e) => {
        if (window.scrollY >= 400) {
            document.getElementById("nav").classList.add("fixed-top");
        }

        if (window.scrollY === 0) {
            document.getElementById("nav").classList.remove("fixed-top");
        }

    });
}

function setPadding() {

    if (window.innerWidth <= 700) {

        document.getElementById("controlPanelAndSlider").style.marginLeft = "0px";
        document.getElementById("controlPanelAndSlider").style.marginRight = "0px";
    }

}


(function showNumberOfProducts() {

    fetch("/api/NumberOfProducts").then(x => x.json()).then(x => {

        let number = JSON.parse(x);

        document.getElementById("cartNumberOfProducts").innerText = number;
    });

})();


setPadding();
navFunction();

