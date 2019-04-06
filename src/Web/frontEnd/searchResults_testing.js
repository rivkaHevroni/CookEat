function GetRecipeElement(recipe){
    var recipeElement = document.createElement('recipe');
    var image = document.createElement('IMG');
    image.src = recipe.Picture;
    recipeElement.appendChild(image);
    return recipeElement;
} 

var jsonText = '[{"_id":5,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":6,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"},{"_id":7,"PreparationTime":200,"Link":"https://www.chef-lavan.co.il/�������/������-�����-������-3-��������","NumberOfDiners":5,"Picture":"https://foodrevolution.org/wp-content/uploads/2018/04/blog-featured-diabetes-20180406-1330.jpg","RecipeTitle":"3����2"}]'
var recipes=JSON.parse(jsonText);
recipes.forEach(recipe => {
    document.getElementById('oran').appendChild(GetRecipeElement(recipe));
});

<recipe>
    <image src = 'shit' />
</recipe>