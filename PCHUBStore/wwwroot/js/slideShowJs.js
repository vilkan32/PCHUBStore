function slideShowEffect() {

    Array.from(document.getElementById('slideShow').children).forEach(x => x.addEventListener('change', changePicture));

    function changePicture(x) {
        let picture = document.getElementById('slideShowPicture');
        let target = x.target;
        clearInterval(window.intervalProp);
        picture.src = '/' + target.value + '.jpg';
    }

}

var intervalProp = setInterval(function () {
    let picture = document.getElementById('slideShowPicture');
    picture.src = '/' + 'option' + randomInteger(1, 3) + '.jpg';
}, 4000);



function randomInteger(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}


