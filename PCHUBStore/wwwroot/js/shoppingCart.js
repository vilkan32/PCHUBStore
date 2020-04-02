function changeQuantity() {

    document.getElementById("quantity").addEventListener("change", (e) => {

        let target = e.target;

        let value = target.value;

        let parent = target.parentElement;

        let brother = parent.children;

        let href = brother[1].href;

        href += "?quantity=" + value;

        brother[1].href = href;

        brother[1].click();

    });
}

changeQuantity();