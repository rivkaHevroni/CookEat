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

        numberOfTextBoxesInDocument++;
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
    var allIngredientFull = true;
    try {
        searchIngredientList = new Array(numberOfTextBoxesInDocument)

        for (var i = 0; i < numberOfTextBoxesInDocument; i++) {
            var elementId = `tb_${i}`;
            if (document.getElementById(elementId).value != "") {
                searchIngredientList[i] = document.getElementById(elementId).value;
                console.log(searchIngredientList[i]);
            }
            else {
                allIngredientFull = false;
            }
        }
        console.log(searchIngredientList);

        if (allIngredientFull == true) {
            var searchRequest = { IngrediantNames: searchIngredientList };

            localStorage.setItem("SearchRequest", JSON.stringify(searchRequest));
            window.location.href = "http://localhost/searchResults.html";
        }
        else {
            alert("לא ניתן להכניס ערך ריק");
        }

    }
    catch (err) {
        alert(err);
    }
})

document.getElementById("QuerySearchButton").addEventListener('click', (clickEvent) =>{

    clickEvent.preventDefault();
    try {
        if (document.getElementById('query').value != "") {
            var searchQuery = document.getElementById('query').value;
            var searchRequest = { SearchQuery: searchQuery };
            localStorage.setItem("SearchRequest", JSON.stringify(searchRequest));
            window.location.href = "http://localhost/searchResults.html";
        }
        else {
            alert("יש להכניס ערך לחיפוש");
        }
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

document.getElementById("imageToSearch").addEventListener('click', (clickEvent) => {
    clickEvent.preventDefault();
        var imgElement = document.getElementById("usersImage");
        var chosenPicBase64 = imgElement.src.split("base64,")[1];
        var searchRequest = { ImageBytes: chosenPicBase64 };
        localStorage.setItem("SearchRequest", JSON.stringify(searchRequest));
        window.location.href = "http://localhost/searchResults.html";
})

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

if (window.FileReader) {
    var drop;
    addEventHandler(window, 'load', function () {
        var status = document.getElementById('status');
        drop = document.getElementById('drop');
        var list = document.getElementById('list');

        function cancel(e) {
            if (e.preventDefault) {
                e.preventDefault();
            }
            return false;
        }

        // Tells the browser that we *can* drop on this target
        addEventHandler(drop, 'dragover', cancel);
        addEventHandler(drop, 'dragenter', cancel);

        addEventHandler(drop, 'drop', function (e) {
            e = e || window.event; // get window.event if e argument missing (in IE)   
            if (e.preventDefault) {
                e.preventDefault();
            } // stops the browser from redirecting off to the image.

            var dt = e.dataTransfer;
            var files = dt.files;
            for (var i = 0; i < files.length; i++) {
                var file = files[i];
                var reader = new FileReader();

                //attach event handlers here...

                reader.readAsDataURL(file);
                addEventHandler(reader, 'loadend', function (e, file) {
                    var bin = this.result;
                    var newFile = document.createElement('div');
                    newFile.innerHTML = 'Loaded : ' + file.name + ' size ' + file.size + ' B';
                    list.appendChild(newFile);

                    var dropElement = document.getElementById("drop");
                    var img = document.createElement("img");
                    img.id = "usersImage";
                    img.file = file;
                    img.src = bin;
                    dropElement.innerHTML = "";
                    dropElement.appendChild(img);

                    var serchButton = document.getElementById("imageToSearch");
                    serchButton.style.pointerEvents = "unset";
                    serchButton.style.opacity = "1";
                }.bindToEventHandler(file));
            }
            return false;
        });
        Function.prototype.bindToEventHandler = function bindToEventHandler() {
            var handler = this;
            var boundParameters = Array.prototype.slice.call(arguments);
            console.log(boundParameters);
            //create closure
            return function (e) {
                e = e || window.event; // get window.event if e argument missing (in IE)   
                boundParameters.unshift(e);
                handler.apply(this, boundParameters);
            }
        };
    });
} else {
    document.getElementById('status').innerHTML = 'Your browser does not support the HTML5 FileReader.';
}

function addEventHandler(obj, evt, handler) {
    if (obj.addEventListener) {
        // W3C method
        obj.addEventListener(evt, handler, false);
    } else if (obj.attachEvent) {
        // IE method.
        obj.attachEvent('on' + evt, handler);
    } else {
        // Old school method.
        obj['on' + evt] = handler;
    }
}
