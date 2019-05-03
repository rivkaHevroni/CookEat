var recipe = '{"_id":"5","PreparationTime":"200","Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":"5","Picture":"https://www.chef-lavan.cso.il/uploads/images/03c867e7ea65eebd00be9e6b0dddf586.jpg","RecipeTitle":"3����2"}';

var recipeObj = JSON.parse(recipe);
    
document.getElementById("recipe_numOfDiners").innerHTML = recipeObj.NumberOfDiners;
