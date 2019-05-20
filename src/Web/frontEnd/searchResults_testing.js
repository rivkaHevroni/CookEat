function GetRecipeElement(recipe){
    var recipeElement = document.createElement('recipe');
    var id = document.createElement('p');
    var image = document.createElement('IMG');
    var recipeTitle = document.createElement('p');
    var ingrediants = document.createElement('div');
    var breakLine = document.createElement('br');
    var preparationTime = document.createElement('div');
    var link = document.createElement('a');
    var numberOfDishes = document.createElement('p');
    numberOfDishes.innerHTML = recipe.numOfDishes;
    link.innerHTML- recipe.link;
    recipeTitle.innerHTML=recipe.recipeTitle;
    id.innerHTML = recipe.id;
    preparationTime.innerHTML = recipe.preparationTime;
    image.src = recipe.picture;
    recipeTitle.style.textAlign = "center";
    recipeTitle.style.fontSize = "14px";
    image.style.marginLeft="auto";
    image.style.marginRight="auto";
    image.style.display="block";
    image.style.width="90%";
    image.style.height= "180px";
    id.style.display="none";
    recipeElement.appendChild(id);
    recipeElement.appendChild(image);
    recipeElement.appendChild(recipeTitle);
    recipeElement.appendChild(breakLine);
    
    return recipeElement;
}

function moreDetailsEvent(id, recipes){
    var i;
    for(i=0; i<recipes.length; i++){
        if(id == recipes[i].id){
            var modalHeader = document.createElement('div');
            modalHeader.setAttribute("class", "modal-header");
            var image1 = document.createElement('IMG');
            image1.src=recipes[i].Picture;
            image1.setAttribute("class", "card-img-top");
            modalHeader.appendChild(image1);
        }
    }
}

function PrintRecipes(recipes) {
    var table = document.createElement('table');

    if (recipes == null || recipes.length == 0) {
        var messege = document.createElement('span');
        messege.innerText = "מצטערים, לא נמצאו מתכונים מתאימים";
        messege.style.margin = "0 auto";
        messege.style.display = "table";
        messege.style.fontSize = "25px";
        document.getElementById("emptyResult").appendChild(messege);

        return table;
    }

    var numOfRows = Math.ceil((recipes.length)/4, 0);
    var currentRecipeIndex = 0;

    for(var currentRow=0; currentRow<numOfRows; currentRow++){
        var row = table.insertRow(currentRow);
        for(var currentCol=0; currentCol<4; currentCol++ , currentRecipeIndex++){
           if(currentRecipeIndex < recipes.length){

                var cell = row.insertCell(currentCol);
                cell.style.border="solid white 20px";
                cell.style.backgroundColor="#F5F5F5";
                cell.appendChild(GetRecipeElement(recipes[currentRecipeIndex]));
                var saveButton = document.createElement('BUTTON');
                saveButton.innerHTML="שמור ";
                saveButton.setAttribute("class", "btn btn-danger");
                var sp1 = document.createElement('span');
                sp1.setAttribute("class", "glyphicon glyphicon-pushpin");
                saveButton.appendChild(sp1);
                cell.appendChild(saveButton);
                var moreDetails = document.createElement('BUTTON');
                moreDetails.setAttribute("class", "btn btn-danger");
                moreDetails.setAttribute("id", recipes[currentRecipeIndex].id);
                moreDetails.innerHTML = "לפרטים נוספים ";
                moreDetails.onclick = function(ev) {
                    moreDetailsOnClick(recipes, ev.target.id)
                }; 
                var sp2 = document.createElement('span');
                sp2.setAttribute("class", "glyphicon glyphicon-plus")
                moreDetails.appendChild(sp2);
                cell.appendChild(moreDetails);
           }
           else{
                var cell = row.insertCell(currentCol);
                var image = document.createElement('IMG');
                image.src="http://www.allwhitebackground.com/images/2/2270.jpg";
                image.style.width="90%";
                image.style.height= "180px";
                cell.appendChild(image);
           }
           
           cell.style.padding="0";
           cell.style.width="400px";
           cell.style.verticalAlign="top";
           cell.style.position="relative";
           saveButton.style.position="absolute";
           saveButton.style.bottom="1px";
           saveButton.style.left="1px";
           saveButton.style.borderRadius="5px";
           saveButton.style.padding="3px";
           saveButton.style.color="white";
           saveButton.style.fontWeight="bold";
           moreDetails.style.position="absolute";
           moreDetails.style.bottom="1px";
           moreDetails.style.left="50px";
           moreDetails.style.borderRadius="5px";
           moreDetails.style.fontWeight="bold";
           moreDetails.style.padding="3px";
        }
    }
    return table;
}

function moreDetailsOnClick(recipes, id){
    var recipeDetails = recipes.find(function(recipe) {
        return recipe.id === id;
      });

    document.getElementById('recipe-modal').style.display = "block"; // Make the modal visble
    document.getElementById("recipe-image").src= recipeDetails.picture;
    document.getElementById("recipe-title").innerHTML = recipeDetails.recipeTitle;
    document.getElementById("time").innerHTML = recipeDetails.preparationTime;
    document.getElementById("recipe-link").href = recipeDetails.link;
    document.getElementById("recipe-link").target ="_blank";
    document.getElementById("recipe-link").innerHTML = "למעבר למתכון המלא לחץ כאן";
    if(recipeDetails.numberOfDishes != 0){
        document.getElementById("numOfDis").innerHTML = recipeDetails.numberOfDishes;
        document.getElementsByClassName("space").innerHTML = "  ";
        document.getElementById("dishes").innerHTML = " מנות ";
    }
}

fetch("http://localhost/api/search", {
        method: "POST",
        headers: {
            "Content-Type": "application/json; charset=UTF-8"
        },
        body: localStorage.getItem("SearchRequest")
    }).
    then(response => response.json()).
    then(searchResponse => document.getElementById('ronen').appendChild(PrintRecipes(searchResponse.results)))

    var enterToPersonalInfoElm = document.getElementById('a');
    enterToPersonalInfoElm.addEventListener('click', (clickEvent) => {
        clickEvent.preventDefault();
        try {
            window.location.href= "http://localhost/index.html";
        }
        catch (err) {
            alert(err);
        }
    })
    
    var enterHomePageElm = document.getElementById('b');
    enterHomePageElm.addEventListener('click', (clickEvent) => {
        clickEvent.preventDefault();
        try {
            window.location.href= "http://localhost/Authentication.html";
        }
        catch (err) {
            alert(err);
        }
    })

    document.getElementById("minus").addEventListener('click', (clickEvent) => {
        {
            clickEvent.preventDefault();
            var numberOfDishes = document.getElementById("numOfDis").textContent;
            numberOfDishes--;
            if(numberOfDishes<=0)
            {
                alert("לא ניתן להציג מספר מנות שלילי");
            }
            else{
                document.getElementById("numOfDis").innerHTML = numberOfDishes;
            }
        }
    })

    document.getElementById("plus").addEventListener('click', (clickEvent) => {
        {
            clickEvent.preventDefault();
            var numberOfDishes = document.getElementById("numOfDis").textContent;
            numberOfDishes++;
            document.getElementById("numOfDis").innerHTML = numberOfDishes;
        }
    })
