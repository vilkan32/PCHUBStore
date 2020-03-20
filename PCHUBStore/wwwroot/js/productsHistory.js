function loadReviewdProducts() {


    fetch("/api/ReviewedProducts").then(res => res.json()).then(x => {
        let obj = x;

        obj.forEach(x => {

            let html = `<div class="col-sm-4 col-lg-2 text-center border bg-white m-2">
                    <div class="container">
                         <a style="width: inherit; height: 3em" href="${x.VisitProductUrl}"> <img style="width: inherit" src="${x.PictureUrl}" alt="Alternate Text" /></a>
                    </div>
                    <div class="container">
                        <p>${x.Title}</p>
                    </div>
                    <div class="container">
                        <p>${x.Price}.<span style="font-size:10px">00</span></p>
                    </div>
                </div>`;

            document.getElementById("reviewedProducts").innerHTML += html;

        });

    });


}

loadReviewdProducts();