var numberOfTextBoxesInDocument = 0;

document.getElementById('add').addEventListener('click', (clickEvent) =>
{
    clickEvent.preventDefault()
    if(numberOfTextBoxesInDocument==5){
        alert("אפשר להוסיף עד כחמישה מצרכים")
    }
    else{
    //create the div that warp up the text box
    var textBoxDivWrapperElement = document.createElement('div');
    textBoxDivWrapperElement.setAttribute('id', `div_${numberOfTextBoxesInDocument}`);

    //create the text box
    var textBoxElement = document.createElement('input')
    textBoxElement.setAttribute('type', 'text')
    textBoxElement.setAttribute('id', `tb_${numberOfTextBoxesInDocument}`)

    textBoxDivWrapperElement.appendChild(textBoxElement)

    document.getElementById('content').appendChild(textBoxDivWrapperElement)

    numberOfTextBoxesInDocument++
    }
})


document.getElementById('remove').addEventListener('click', (clickEvent) =>
{
    clickEvent.preventDefault()
    if(numberOfTextBoxesInDocument > 0)
    {
        document.getElementById('content').removeChild(document.getElementById(`div_${numberOfTextBoxesInDocument-1}`))
        numberOfTextBoxesInDocument--
    }
    else
    {
        window.alert('No textbox to remove')
    }
})

document.getElementById('save').addEventListener('click', (clickEvent) =>
{
    clickEvent.preventDefault()
    try {
        searchIngredientList = new Array(numberOfTextBoxesInDocument)

        for (var i = 0; i < numberOfTextBoxesInDocument; i++) {
            var elementId = `tb_${i}`;
            searchIngredientList[i] = document.getElementById(elementId).value;
            console.log(searchIngredientList[i]);
        }
        console.log(searchIngredientList);

        var searchRequest = { IngrediantNames: searchIngredientList };

        localStorage.setItem("SearchRequest", JSON.stringify(searchRequest));
        window.location.href = "http://localhost/searchResults_testing.html";
    }
    catch (err) {
        alert(err);
    }
})

document.getElementById("QuerySearchButton").addEventListener('click', (clickEvent) =>
{
    clickEvent.preventDefault();
    try {
        var searchQuery = document.getElementById('query').value;
        var searchRequest = {SearchQuery: searchQuery};
        localStorage.setItem("SearchRequest", JSON.stringify(searchRequest));
        window.location.href = "http://localhost/searchResults_testing.html";
    }
    catch(err) {
        alert(err);
    }
})

document.getElementById("personalAreaButton").addEventListener('click', (clickEvent) =>
{
    clickEvent.preventDefault();
    window.location.href= "http://localhost/personalArea.html";  
})

function searchByQuery(searchQuery){
    
    //return (sendSearchRequest(searchRequest)).Results;
}

function sendSearchRequest(request) {
    return fetch("http://localhost/search", {
        method: "POST",
        headers: {
            "Content-Type": "application/json; charset=UTF-8"
        },
        body: JSON.stringify(request)
    }).
    then(response => response.json()).
    then(obj => Console.log(JSON.stringify(obj)));
}

/*document.getElementById("imageToSearch").addEventListener('click', (clickEvent) =>
{
    clickEvent.preventDefault();
    var file= $('file')[0].files[0];
    try {
        const buffer = await sharp(file).png().toBuffer()
        alert(buffer);
    }
    catch(err) {
        alert(err);
    }
})*/

/*document.getElementById("imageToSearch").addEventListener('click', (clickEvent) =>
{
})*/

document.getElementById("QuerySearchButton").addEventListener('click', (clickEvent) => {
    clickEvent.preventDefault();
    try {
        var searchQuery = document.getElementById('query').value;
        var searchRequest = { SearchQuery: searchQuery };
        localStorage.setItem("SearchRequest", JSON.stringify(searchRequest));
        window.location.href = "http://localhost/searchResults_testing.html";
    }
    catch (err) {
        alert(err);
    }
})
