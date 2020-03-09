function loadMainSliderPictures() {



    let regex = /option\d/;

    fetch("/api/index/slider").then(res => res.json()).then(x => {
        let obj = JSON.parse(x);
        let pictures = obj["Pictures"].filter(x => regex.exec(x.Name) && x.IsDeleted === false);
        document.getElementById('mainSlidePic').src = pictures[randomInteger(0, 3)].Url;
        setInterval(function () {

            document.getElementById('mainSlidePic').src = pictures[randomInteger(0, 3)].Url;

        }, 4000);
 

    });


}


function onMouseInMainSlider() {


    let mainSlider = document.getElementById('optionsOrSlider');

    mainSlider.addEventListener('mouseenter', function (e) {


        document.getElementById('leftArrow').style.display = 'block';
        document.getElementById('leftArrow').style.background = '#F2F1F0';
        document.getElementById('leftArrow').style.border = 'solid';
        document.getElementById('leftArrow').style.borderColor = 'mediumseagreen';
        document.getElementById('leftArrow').style.borderLeftColor = '#F2F1F0';

        document.getElementById('rightArrow').style.display = 'block';
        document.getElementById('rightArrow').style.background = '#F2F1F0';
        document.getElementById('rightArrow').style.border = 'solid';
        document.getElementById('rightArrow').style.borderColor = 'mediumseagreen';
        document.getElementById('rightArrow').style.borderRightColor = '#F2F1F0';



    });

    mainSlider.addEventListener('mouseleave', function (e) {


        document.getElementById('leftArrow').style.display = 'none';
     
        document.getElementById('rightArrow').style.display = 'none';



    });

}

function randomInteger(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function setSliderHeight() {

    let cpHeight = document.getElementById("controlPanel").offsetHeight;
    document.getElementById("controlPanelAndSlider").style.minHeight = cpHeight + "px";
    document.getElementById("mainSlidePic").style.minHeight = cpHeight + "px";
}

setSliderHeight();
