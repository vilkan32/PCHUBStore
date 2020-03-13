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

miniPicturesChangeBorder();