(function loadMainSliderPictures() {

    fetch("/api/GetMainSliderPictures").then(res => res.json()).then(x => {
        let pictures = x;
        let index = randomInteger(0, pictures.length - 1);
        document.getElementById('mainSlidePic').src = pictures[index].url;
        document.getElementById('mainSlidePic').setAttribute("value", index);

       window.intervals = setInterval(function () {
            index = randomInteger(0, pictures.length - 1);
            if (Number(document.getElementById('mainSlidePic').getAttribute("value")) === index) {
                index = randomInteger(0, pictures.length - 1);
                document.getElementById('mainSlidePic').setAttribute("value", index);
                document.getElementById('mainSlidePic').src = pictures[index].url;
            } else {
                document.getElementById('mainSlidePic').setAttribute("value", index);
                document.getElementById('mainSlidePic').src = pictures[index].url;
            }
        }, 10000);

    });

})();


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

(function mainSliderArrows() {

    document.getElementById('leftArrow').addEventListener("click", () => {
        fetch("/api/GetMainSliderPictures").then(res => res.json()).then(x => {

            let pictures = x;
 
                clearInterval(window.intervals);
         
            let index = Number(document.getElementById('mainSlidePic').getAttribute("value"));
            index -= 1;
            if (index === -1) {
                index = pictures.length - 1;
                document.getElementById('mainSlidePic').src = pictures[index].url;
                document.getElementById('mainSlidePic').setAttribute("value", index);

            } else {
                document.getElementById('mainSlidePic').src = pictures[index].url;
                document.getElementById('mainSlidePic').setAttribute("value", index);
            }

          
               
        });
    });

    document.getElementById('rightArrow').addEventListener("click", () => {
        fetch("/api/GetMainSliderPictures").then(res => res.json()).then(x => {

            let pictures = x;

            clearInterval(window.intervals);

            let index = Number(document.getElementById('mainSlidePic').getAttribute("value"));
            index += 1;
            if (index === pictures.length) {
                index = 0;
                document.getElementById('mainSlidePic').src = pictures[index].url;
                document.getElementById('mainSlidePic').setAttribute("value", index);

            } else {
                document.getElementById('mainSlidePic').src = pictures[index].url;
                document.getElementById('mainSlidePic').setAttribute("value", index);
            }

          

        });
    });

})();


function randomInteger(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function setSliderHeight() {

    let cpHeight = document.getElementById("controlPanel").offsetHeight;

    document.getElementById("controlPanelAndSlider").style.minHeight = cpHeight + "px";
    document.getElementById("mainSlidePic").style.minHeight = cpHeight + "px";

}

setSliderHeight();
onMouseInMainSlider();