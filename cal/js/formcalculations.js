//Set up an associative array
//The keys represent the size of the cake
//The values represent the cost of the cake i.e A 10" cake cost's $35


//        var photo_prices = new Array();
//        photo_prices["Round6"]=20;
//        photo_prices["Round8"]=25;
//        photo_prices["Round10"]=35;
//        photo_prices["Round12"]=75;


var photo_prices = new Array();
photo_prices["None"] = 0;
photo_prices["1"] = 80;
photo_prices["2"] = 110;
photo_prices["3"] = 145;
photo_prices["4"] = 170;
photo_prices["5"] = 495;
photo_prices["6"] = 425;
photo_prices["7"] = 350;
photo_prices["8"] = 275;
photo_prices["9"] = 195;



//Set up an associative array 
//The keys represent the filling type
//The value represents the cost of the filling i.e. Lemon filling is $5,Dobash filling is $9
//We use this this array when the user selects a filling from the form
var quantity_prices = new Array();
quantity_prices["None"] = 0;
quantity_prices["1"] = 1;
quantity_prices["2"] = 2;
quantity_prices["3"] = 3;
quantity_prices["4"] = 4;
quantity_prices["5"] = 5;
quantity_prices["6"] = 6;
quantity_prices["7"] = 7;
quantity_prices["8"] = 8;
quantity_prices["9"] = 9;
quantity_prices["10"] = 10;
quantity_prices["11"] = 11;
quantity_prices["12"] = 12;



// getCakeSizePrice() finds the price based on the size of the cake.
// Here, we need to take user's the selection from radio button selection
function getPhotoSizePrice() {
    var PhotoSizePrice = 0;
    //Get a reference to the form id="cakeform"
    var theForm = document.forms["photoform"];
    //Get a reference to the cake the user Chooses name=selectedCake":
    var selectedSize = theForm.elements["selectedsize"];
    //Here since there are 4 radio buttons selectedCake.length = 4
    //We loop through each radio buttons
    for (var i = 0; i < selectedSize.length; i++) {
        //if the radio button is checked
        if (selectedSize[i].checked) {
            //we set cakeSizePrice to the value of the selected radio button
            //i.e. if the user choose the 8" cake we set it to 25
            //by using the cake_prices array
            //We get the selected Items value
            //For example cake_prices["Round8".value]"
            PhotoSizePrice = photo_prices[selectedSize[i].value];
            //If we get a match then we break out of this loop
            //No reason to continue if we get a match
            break;
        }
    }
    //We return the cakeSizePrice
    return PhotoSizePrice;
}

//This function finds the filling price based on the 
//drop down selection
function getQuantityPrice() {
    var myQunatityPrice = 0;
    //Get a reference to the form id="cakeform"
    var theForm = document.forms["photoform"];
    //Get a reference to the select id="filling"
    var selectedQuantity = theForm.elements["quantitywant"];

    //set cakeFilling Price equal to value user chose
    //For example filling_prices["Lemon".value] would be equal to 5
    myQunatityPrice = quantity_prices[selectedQuantity.value];

    //finally we return cakeFillingPrice
    return myQunatityPrice;
}




function calculateTotal() {
    //Here we get the total price by calling our function
    //Each function returns a number so by calling them we add the values they return together
    var mytotalPrice = getPhotoSizePrice() * getQuantityPrice();

    //display the result
    var divobj = document.getElementById('totalPrice');
    divobj.style.display = 'block';
    //divobj.innerHTML = mytotalPrice;
    divobj.value = mytotalPrice;

}

function hideTotal() {
    var divobj = document.getElementById('totalPrice');
    divobj.style.display = 'none';
}