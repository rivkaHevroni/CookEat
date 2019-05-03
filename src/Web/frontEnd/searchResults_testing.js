function GetRecipeElement(recipe){
    var recipeElement = document.createElement('recipe');
    var image = document.createElement('IMG');
    var recipeTitle = document.createElement('p');
    var breakLine = document.createElement('br');
    recipeTitle.innerHTML=recipe.RecipeTitle;
    image.src = recipe.Picture;
    recipeTitle.style.textAlign="center";
    image.style.marginLeft="auto";
    image.style.marginRight="auto";
    image.style.display="block";
    image.style.width="90%";
    image.style.height= "180px";
    recipeElement.appendChild(image);
    recipeElement.appendChild(recipeTitle);
    recipeElement.appendChild(breakLine);
    
    return recipeElement;
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
                saveButton.innerHTML="שמור";
                var sp1 = document.createElement('span');
                sp1.setAttribute("class", "glyphicon glyphicon-pushpin");
                saveButton.appendChild(sp1);
                cell.appendChild(saveButton);
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
           saveButton.style.backgroundColor="red";
           saveButton.style.padding="3px";
           saveButton.style.color="white";
           saveButton.style.fontWeight="bold";
           numOfRecipe++;
        }
    }
    return table;
}

var jsonText = '[{"_id":5,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3���2 hghgh hjghjgj jgjgj jgjg hgjhg jhgjgjj jhghgjh hfhg nhghn hgjhg jgtjhg jgtujg jgjgj kjgj"},{"_id":6,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":7,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"}, {"_id":5,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":6,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":7,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"}, {"_id":5,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":6,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":7,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://www.akc.org/wp-content/themes/akc/component-library/assets/img/welcome.jpg","RecipeTitle":"3����2"}]'
var recipes=JSON.parse(jsonText);
document.getElementById('ronen').appendChild(PrintRecipes(recipes));





