function GetRecipeElement(recipe){
    var recipeElement = document.createElement('recipe');
    var id = document.createElement('p');
    var image = document.createElement('IMG');
    var recipeTitle = document.createElement('p');
    var breakLine = document.createElement('br');
    recipeTitle.innerHTML=recipe.recipeTitle;
    id.innerHTML = recipe.id;
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
        if(id == recipes[i]._id){
            var modalHeader = document.createElement('div');
            modalHeader.setAttribute("class", "modal-header");
            var image1 = document.createElement('IMG');
            image1.src=recipes[i].Picture;
            image1.setAttribute("class", "card-img-top");
            modalHeader.appendChild(image1);
        }
    }
}

function PrintRecipes(recipes){
    if (recipes == null) {
        var messege = document.createElement('span');
        messege.innerText = "מצטערים, לא נמצאו מתכונים מתאימים";
        messege.style.margin = "0 auto";
        messege.style.display = "table";
        messege.style.fontSize = "25px";
        document.getElementById("emptyResult").appendChild(messege);
    }
    var numOfRecipe = 0;
    var numOfRows = Math.ceil((recipes.length)/4, 0);
    var table = document.createElement('table');
    for(var i=0; i<numOfRows; i++){
        var row = table.insertRow(i);
        for(var j=0; j<4; j++){
           if(numOfRecipe<recipes.length){
                var cell = row.insertCell(j);
                cell.style.border="solid white 20px";
                cell.style.backgroundColor="#F5F5F5";
                cell.appendChild(GetRecipeElement(recipes[numOfRecipe]));
                var saveButton = document.createElement('BUTTON');
                saveButton.innerHTML="שמור ";
                saveButton.setAttribute("class", "btn btn-danger");
                var sp1 = document.createElement('span');
                sp1.setAttribute("class", "glyphicon glyphicon-pushpin");
                saveButton.appendChild(sp1);
                cell.appendChild(saveButton);
                var moreDetails = document.createElement('BUTTON');
                moreDetails.setAttribute("class", "btn btn-danger");
                moreDetails.innerHTML = "לפרטים נוספים ";
                //moreDetails.setAttribute("id", "5");
                var demo = recipes[i]._id;
                moreDetails.onclick = function(){moreDetailsEvent(demo, recipes);};
                var sp2 = document.createElement('span');
                sp2.setAttribute("class", "glyphicon glyphicon-plus")
                moreDetails.appendChild(sp2);
                cell.appendChild(moreDetails);
                //var demo = document.getElementById(recipes[i]._id);
           }
           else{
                var cell = row.insertCell(j);
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
           numOfRecipe++;
        }
    }
    return table;
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






