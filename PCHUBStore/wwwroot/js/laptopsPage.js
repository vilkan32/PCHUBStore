function changeLaptopsBorderOnHover() {

    Array.from(document.getElementsByClassName("laptopsJs")).forEach(x => x.addEventListener('mouseenter', changeBorder));


    Array.from(document.getElementsByClassName("laptopsJs")).forEach(x => x.addEventListener('mouseleave', revertBorder));

    function changeBorder(e) {

        let target = e.target;

        target.style.borderColor = 'orange';

        target.lastElementChild.style.visibility = 'visible';
    }


    function revertBorder(e) {

        let target = e.target;

        target.style.borderColor = 'white';

        
        target.lastElementChild.style.visibility = 'hidden';
    }

}

changeLaptopsBorderOnHover();