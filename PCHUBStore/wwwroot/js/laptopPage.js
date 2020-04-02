$(document).ready(function () {
		$('#mainPicture').zoom();
	});


function miniPicturesChangeBorder() {

	Array.from(document.getElementsByClassName('miniPictures')).forEach(x => x.addEventListener('mouseenter', function (e) {

		e.target.style.borderColor = 'orange';

	}));

	Array.from(document.getElementsByClassName('miniPictures')).forEach(x => x.addEventListener('mouseleave', function (e) {

		e.target.style.borderColor = 'white';

	}));

	Array.from(document.getElementsByClassName('miniPictures')).forEach(x => x.addEventListener('click', function (e) {
	
		let targetSrc = e.target.src;
		document.getElementById('mainPicture').firstElementChild.src = targetSrc;
		document.getElementById('mainPicture').lastElementChild.src = targetSrc;

	}));
}


function addToFavorites() {

	document.getElementById("addToFavorites").addEventListener('click', (e) => {

		let id = e.target.value;

		fetch(`/api/Favorites?id=${id}`).then(x => x.text()).then(x => {


			if (x === "true") {

				let intervalNone = setInterval(() => {

					document.getElementById('favoriteImg').style.visibility = 'hidden';

				}, 500);

				let intervalBlock = setInterval(() => {

					document.getElementById('favoriteImg').style.visibility = 'visible';

				}, 1020);

				setTimeout(() => {

					clearInterval(intervalNone);
					clearInterval(intervalBlock);
					document.getElementById('favoriteImg').style.visibility = 'visible';


				}, 4000);
			} else {

				document.getElementById("messageParagraph").innerHTML = "The product has already been added!!";

				setTimeout(() => { document.getElementById("messageParagraph").innerHTML = ""; }, 4000);
			} 


		});

	});

}


try {
	miniPicturesChangeBorder();
} catch (e) {
	console.log("Error");
}

try {
	addToFavorites();
} catch (e) {
	console.log("Error");
}

