// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const wElement = document.getElementById("welcome");
const wText = wElement.innerText;
const newText = "Welcome, you";
const upper_letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ", lower_letters = "abcdefghijklmnopqrstuvwxyzê";
wElement.addEventListener('mouseover', handleMouseOver);

let iteration = 0;
function handleMouseOver() {
    console.log('Mouse overed!');
    interval = setInterval(() => {
        wElement.innerText = wText
        .split("")
        .map((letter, index) => {
            if (index < iteration) { return newText[index]; }
            if (Math.abs(index - iteration) > 3) {
                if (upper_letters.includes(wText[index])) { return upper_letters[Math.floor(Math.random() * 26)]; }
                if (lower_letters.includes(wText[index])) { return lower_letters[Math.floor(Math.random() * 27)]; }
            }
            return wText[index];
        }).join("");
        iteration += 1/4;
        if (iteration > wText.length) { clearInterval(interval); }
    }, 50);
    iteration = 0;
    wElement.removeEventListener('mouseover', handleMouseOver);
}
