function loadReviewdProducts() {


    fetch("/api/ReviewedProducts").then(res => res.json()).then(x => {
        let obj = x;
        obj.forEach(z => {

            let html = `<div class="col-sm-4 col-lg-2 text-center border bg-white m-2">
                    <div class="container">
                         <a style="width: inherit; height: 3em" href="${z.visitProductUrl}"> <img style="width: inherit" src="${z.pictureUrl}" alt="Alternate Text" /></a>
                    </div>
                    <div class="container">
                        <p>${z.title}</p>
                    </div>
                    <div class="container">
                        <p>${z.price}.<span style="font-size:10px">00</span></p>
                    </div>
                </div>`;

            document.getElementById("reviewedProducts").innerHTML += html;

        });

    });


}

loadReviewdProducts();