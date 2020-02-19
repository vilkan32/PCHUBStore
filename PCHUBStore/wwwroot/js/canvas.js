function canvasExex() {

    var canvas = document.getElementById("canvas");
    var ctx = canvas.getContext("2d");

    img = new Image();
    leftArrow = new Image();

    img.onload = function () {
        canvas.width = 600;
        canvas.height = 500;
        ctx.drawImage(img, 0, 0, img.width, img.height, 0, 0, 600, 500);
        ctx.drawImage(leftArrow, 0, 0, img.width, img.height, 0, 0, 100, 100);
    };
    img.src = "https://smartnews.bg/wp-content/uploads/grand-theft-auto-v-wallp-2013.jpg";
    leftArrow.src = "/arrow-circle-left-solid.svg";
    leftArrow.addEventListener('click', () => { console.log("inside"); });
}

canvasExex();


