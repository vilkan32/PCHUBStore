function expand() {


    Array.from(document.getElementsByClassName('expandable')).forEach(x => x.addEventListener('click', (e) => {

        let target = e.target;

        
        let commands = target.innerText.split(' ');

        let expandable = commands[0];

        let className = commands[1];

        if (expandable === 'Expand') {

            target.innerText = target.innerText.replace("Expand", "Collapse");

            Array.from(document.getElementsByClassName(className)).forEach(x => x.style.display = 'flex');
        } else {

            target.innerText = target.innerText.replace("Collapse", "Expand");

            Array.from(document.getElementsByClassName(className)).forEach(x => x.style.display = 'none');
        }

       

    }));


}

expand();