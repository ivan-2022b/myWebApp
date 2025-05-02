var myData = [];
var bitArray = [];
var bitColumns = 0;
var bitRows = 0;
console.log("before function . .");

var dbRowCount = 0;
var myHeader = document.getElementById("output-header");
const myHeader_text = myHeader.textContent;

function gridSearch() {
    var grid = document.getElementById("myGrid-container");
    console.log("islandScript.js logging . .");
    bitColumns = parseInt(document.getElementById("inputColumns").value);
    console.log(bitColumns);
    bitRows = parseInt(document.getElementById("inputRows").value);
    console.log(bitRows);
    var _bitColumns = ""
    for (var i = 0; i < bitColumns; i++)
        _bitColumns += "60px "
    _bitColumns.trim();

    // clear existing myGrid-container
    while (grid.firstChild != null) {
        grid.removeChild(grid.firstChild);
    }

    // create grid w/ specific columns
    var divGrid = document.createElement("div");
    divGrid.setAttribute("class", "dynamic-grid");
    divGrid.setAttribute("id", "myGrid");
    divGrid.setAttribute("style", "grid-template-columns: " + _bitColumns);

    // pre-loop variables
    var countIslands = 0;
    bitArray = new Array(bitColumns * bitRows * 2);
    // create grid elements
    for (var i = 0; i < bitColumns * bitRows; i++) {
        var divElement = document.createElement("div");
        divElement.setAttribute("class", "grid-styling");
        divElement.id = "griditem" + i;
        divGrid.appendChild(divElement);
        myData.push("");
        myData[i] = "A";
        if(Math.floor(Math.random() * 8) > 3) {
            bitArray[i] = true;
            divElement.style = "background: linear-gradient(315deg, darkslategray 9%, goldenrod 9%, rgb(255, 191, 16) 50%, bisque 75%); background-size: 300% 300%; background-position: 100% 100%;";
        }
        else {
            bitArray[i] = false;
            divElement.textContent = "*";
        }
        bitArray[i + bitArray.length / 2] = false;
    }
    grid.appendChild(divGrid);
    console.log(myData);
    console.log(bitArray.join(", "));

    for (var i = 0; i < bitColumns * bitRows; i++) {
        console.log("entering for-loop");
        if (!bitArray[i + (bitArray.length / 2)]) { // Verify it's unchecked
            console.log("verified as unchecked");
            bitArray[i + (bitArray.length / 2)] = true; // Mark it as checked
            if (bitArray[i]) { // Check if it's an island
                console.log("verified as island");
                console.log("Island Set %i . .", ++countIslands);
                RecursiveIsland(i); // Mark continuously adjacent island bits
            }
        }
    }
    console.log("Islands: %i", countIslands);

    switch (countIslands) {
        case 0: myHeader.textContent = myHeader_text + " found no islands!"; break;
        case 1: myHeader.textContent = myHeader_text + " found 1 island!"; break;
        default: myHeader.textContent = myHeader_text + " found " + countIslands + " islands!";
    }

    sendData(bitColumns, bitRows, countIslands);
}

function RecursiveIsland(index)
{
    bitArray[index + (bitArray.length / 2)] = true; // Mark it as checked
    
    if (index % bitColumns != bitColumns - 1) { // Verify LEFT bit isn't out of bounds
        if (!bitArray[index + 1 + (bitArray.length / 2)] && bitArray[index + 1]) { // Verify it's unchecked & island
            console.log("0b%i went LEFT, ", index);
            RecursiveIsland(index + 1); // RecursiveIsland it
        }
    }
    if (index + bitColumns < bitArray.length / 2) { // Verify DOWN bit isn't out of bounds
        if (!bitArray[index + bitColumns + (bitArray.length / 2)] && bitArray[index + bitColumns]) { // Verify it's unchecked & island
            console.log("0b%i went DOWN, ", index);
            RecursiveIsland(index + bitColumns); // RecursiveIsland it
        }
    }
    if (index % bitColumns != 0) { // Verify RIGHT bit isn't out of bounds
        if (!bitArray[index - 1 + (bitArray.length / 2)] && bitArray[index - 1]) { // Verify it's unchecked & island
            console.log("0b%i went RIGHT, ", index);
            RecursiveIsland(index - 1); // RecursiveIsland it
        }
    }
    if (index - bitColumns > 0) { // Verify UP bit isn't out of bounds
        if (!bitArray[index - bitColumns + (bitArray.length / 2)] && bitArray[index - bitColumns]) { // Verify it's unchecked & island
            console.log("0b%i went UP, ", index);
            RecursiveIsland(index - bitColumns); // Run RecursiveIsland
        }
    }
    console.log("0b%i went BACK, ", index);
}

async function getData() {
    try {
        const response = await fetch('http://localhost:3000/api/mytable/count');
        if (!response.ok) {
            throw new Error(`Response status: ${response.status}`);
        }
        const json = await response.json();
        console.log(json);
        return json[0].no_of_rows;
    } catch (error) {
        console.error(error.message);
    }
}
    
async function sendData(columns, rows, islands) {
    const id = await getData();
    const data = {
        id: id,
        grid_size: "" + columns + "x" + rows,
        island_count: islands
    }
    
    fetch ('http://localhost:3000/api/mytable', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
}

function refreshEvents() {
    const elements = document.getElementsByClassName("grid-styling");
    for (let i = 0; i < elements.length; i++) {
        elements[i].addEventListener('animationend', function(e) {
            elements[i].classList.remove('animated-b');
        });
        elements[i].addEventListener('mouseover', function(e) {
            elements[i].classList.add('animated-b');
        });
    }
}
