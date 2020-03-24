function applyTemplateOnForm() {

// js-basic-key
// js-full-key
// applyCharacteristics

    Array.from(document.getElementsByClassName('applyCharacteristics')).forEach(x => x.addEventListener('click', (e) => {


        let value = e.target.value;
        fetch(`/api/ApplyCharacteristics?category=${value}`).then(x => x.json()).then(x => {

            let bc = x.basicChar;
            let fc = x.fullChar;
            let index = Number(0);

            let basicCharacteristicsInput = Array.from(document.getElementsByClassName('js-basic-key')).forEach(x => {

                x.value = bc[index]; 

                index++;

            });

            index = Number(0);

            let maxCount = x.fullChar.length;

            let fullCharacteristicsInput = Array.from(document.getElementsByClassName('js-full-key')).forEach(x => {
                if (index < maxCount) {

                    x.value = fc[index];

                    index++;
                }

            });

        });



    }));

 //   fetch('/api/ApplyCharacteristics?category=Laptops').then(x => x.json()).then(x => console.log(x.fullChar));

}

applyTemplateOnForm();