function createRecipeResult(recipes){
    for(var i=0; i<recipes.length; i++){

    }
    var modalHeader = document.createElement('div');
    modalHeader.setAttribute("class", "modal-header");
    var image1 = document.createElement('IMG');
    image1.src=recipes[0].Picture;
    image1.setAttribute("class", "card-img-top");
    modalHeader.appendChild(image1);
}