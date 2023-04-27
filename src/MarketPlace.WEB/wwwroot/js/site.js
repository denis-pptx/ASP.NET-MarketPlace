
//remove active from buttons in 160 ms
const buttons = document.querySelectorAll('.btn');

buttons.forEach(button => {
    button.addEventListener('click', () => {

        setTimeout(() => {
            button.blur();
        }, 160);
    });
});
