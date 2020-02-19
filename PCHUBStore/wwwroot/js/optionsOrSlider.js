function changeBigSlider() {

    Array.from(document.querySelectorAll('#controlPanel .nav-item')).forEach(x => x.addEventListener('click', alterContetntSlider));

}

function alterContetntSlider(x) {

    const html = `<div class="col">
                    <section class="brands col-2">
                        <h5>Марки</h5>
                        <ul class="brands" aria-hidden="true">
                            <li><a href="/laptops-all" tabindex="-1">Всички</a></li>
                            <li><a href="/laptops-dell" tabindex="-1">Dell</a></li>
                            <li><a href="/laptops-asus" tabindex="-1">Asus</a></li>
                            <li><a href="/laptops-msi" tabindex="-1">MSI</a></li>
                            <li><a href="/laptops-acer" tabindex="-1">Acer</a></li>
                            <li><a href="/laptops-hp" tabindex="-1">HP</a></li>
                            <li><a href="/laptops-lenovo" tabindex="-1">Lenovo</a></li>
                            <li><a href="/laptops-apple" tabindex="-1">Apple</a></li>
                            <li><a href="/laptops-prestigio" tabindex="-1">Prestigio</a></li>
                            <li><a href="/laptops-alienware" tabindex="-1">Alienware</a></li>
                            <li><a href="/laptops-microsoft" tabindex="-1">Microsoft</a></li>
                            <li><a href="/laptops-fujitsu" tabindex="-1">Fujitsu</a></li>
                            <li><a href="/laptops-toshiba" tabindex="-1">Toshiba</a></li>
                            <li><a href="/laptops-gigabyte" tabindex="-1">Gigabyte</a></li>
                        </ul>
                    </section>
                </div>
                <div class="col">
                    <section>
                        <h5>Цена</h5>
                        <ul aria-hidden="true">
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=300__499&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">300 - 499 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=500__599&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">500 - 599 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=600__799&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">600 - 799 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=800__999&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">800 - 999 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=1000__1199&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">1000 - 1199 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=1200__1499&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">1200 - 1499 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?page=2&amp;search%5Bprice_btw%5D%5B%5D=1500__1999&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">1500 - 1999 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?utf8=%E2%9C%93&amp;search%5Bprice_btw%5D%5B%5D=2000__2499&amp;search%5Bprice_btw%5D%5B%5D=2500__2999&amp;search%5Bprice_btw%5D%5B%5D=3000__4999&amp;search%5Bprice_btw%5D%5B%5D=5000__9999&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;brand_id_in_all=all&amp;type_laptop_id_in_all=all&amp;used_for_type_id_in_all=all&amp;basic_color_id_in_all=all&amp;cpu_type_id_in_all=all&amp;ram_type_capacity_id_in_all=all&amp;vga_type_id_in_all=all&amp;hdd_type_size_id_in_all=all&amp;ssd_size_id_in_all=all&amp;display_size_type_btw_all=all&amp;display_type_id_in_all=all&amp;resolution_size_id_in_all=all&amp;battery_type_id_in_all=all&amp;weight_type_btw_all=all&amp;os_type_id_in_all=all&amp;warranty_size_btw_all=all&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">над 2000 лв.</a></li>
                        </ul>

                    </section>
                </div>
                <div class="col">
                    <section>
                        <h5>Цена</h5>
                        <ul aria-hidden="true">
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=300__499&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">300 - 499 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=500__599&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">500 - 599 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=600__799&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">600 - 799 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=800__999&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">800 - 999 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=1000__1199&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">1000 - 1199 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?search%5Bprice_btw%5D%5B%5D=1200__1499&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">1200 - 1499 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?page=2&amp;search%5Bprice_btw%5D%5B%5D=1500__1999&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">1500 - 1999 лв.</a></li>
                            <li><a href="https://laptop.bg/laptops-all?utf8=%E2%9C%93&amp;search%5Bprice_btw%5D%5B%5D=2000__2499&amp;search%5Bprice_btw%5D%5B%5D=2500__2999&amp;search%5Bprice_btw%5D%5B%5D=3000__4999&amp;search%5Bprice_btw%5D%5B%5D=5000__9999&amp;search%5Bprice_gte%5D=&amp;search%5Bprice_lte%5D=&amp;brand_id_in_all=all&amp;type_laptop_id_in_all=all&amp;used_for_type_id_in_all=all&amp;basic_color_id_in_all=all&amp;cpu_type_id_in_all=all&amp;ram_type_capacity_id_in_all=all&amp;vga_type_id_in_all=all&amp;hdd_type_size_id_in_all=all&amp;ssd_size_id_in_all=all&amp;display_size_type_btw_all=all&amp;display_type_id_in_all=all&amp;resolution_size_id_in_all=all&amp;battery_type_id_in_all=all&amp;weight_type_btw_all=all&amp;os_type_id_in_all=all&amp;warranty_size_btw_all=all&amp;search%5Bmeta_sort%5D=promo_asc" tabindex="-1">над 2000 лв.</a></li>
                        </ul>

                    </section>
                </div>`;

    let optionsOrSliderTagName = document.getElementById('optionsOrSlider').children[0].tagName;

    if (optionsOrSliderTagName === "IMG") {
        let elementToDestroy = document.getElementById('mainSlidePic');
        document.getElementById('optionsOrSlider').removeChild(elementToDestroy);
        document.getElementById('optionsOrSlider').innerHTML = html;
    }

}

changeBigSlider();