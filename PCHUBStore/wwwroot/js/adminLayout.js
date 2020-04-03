(function layout() {

    fetch("/api/AdminLayout").then(x => x.json()).then(x => {

        let data = x;
        document.getElementById("layoutName").innerText = data.username;
        document.getElementById("layoutPicture").src = data.pictureUrl;
    });

})();
