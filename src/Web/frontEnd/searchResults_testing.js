function GetRecipeElement(recipe){
    var recipeElement = document.createElement('recipe');
    var id = document.createElement('p');
    var image = document.createElement('IMG');
    var recipeTitle = document.createElement('p');
    var breakLine = document.createElement('br');
    recipeTitle.innerHTML=recipe.RecipeTitle;
    id.innerHTML = recipe._id;
    image.src = recipe.Picture;
    recipeTitle.style.textAlign="center";
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
    debugger;
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
                debugger;
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

var jsonText = '[{"_id":5,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3���2 hghgh hjghjgj jgjgj jgjg hgjhg jhgjgjj jhghgjh hfhg nhghn hgjhg jgtjhg jgtujg jgjgj kjgj"},{"_id":6,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":7,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"}, {"_id":9,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":10,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":12,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"}, {"_id":11,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":14,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":15,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://www.akc.org/wp-content/themes/akc/component-library/assets/img/welcome.jpg","RecipeTitle":"3����2"}]'
var recipes=JSON.parse(jsonText);
debugger;
document.getElementById('ronen').appendChild(PrintRecipes(recipes));





