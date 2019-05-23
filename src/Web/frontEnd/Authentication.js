document.getElementById("loginButton").addEventListener('click', (clickEvent) =>
{
    clickEvent.preventDefault();
    console.log("hey");
try {
        userName = document.getElementById('loginUserName').value;
        password = document.getElementById('loginPassword').value;
        var position = userName.search(/[\u0590-\u05FF]/); //check if there is hebrew characters in this string
        if(userName == ""){
            alert("אנא הכנס שם משתמש")
            return
        }
        else if(password==""){
            alert("אנא הכנס סיסמא")
            return
        }
        else if (position >=0) {
            alert("אנא הזן שם משתמש באותיות אנגליות בלבד")
            return
        }
        var authenticationRequest= {
            UserName: userName,
            Password: password
        };
        fetch('http://localhost/api/UserProfile/Login', {
            method: 'POST',
            headers: {
                'Content-Type':'application/json',
            }, 
            body: JSON.stringify(authenticationRequest)
    }).
    then(response => response.json()).
    then(authenticationResponse => {
        console.log(authenticationResponse.authenticationResult);
        if(authenticationResponse.authenticationResult == "Success"){
            localStorage.setItem("userName", authenticationRequest.UserName);
            window.location.href= "http://localhost/personalArea.html";
        }
        else{

            if(authenticationResponse.authenticationResult=='UserDoesNotExist')
            {
                alert("משתמש אינו קיים")
            }
            else{
                alert("סיסמא שגויה")
            }
        }
    }).
    catch(err => console.log(err));
}  
    catch(err) {
        alert(err);
    }
})

document.getElementById("registerButton").addEventListener('click', (clickEvent) =>
{
    clickEvent.preventDefault();

    console.log("hey");
try {
        userName = document.getElementById('registerUserNum').value;
        password = document.getElementById('registerPassword').value;
        var position = userName.search(/[\u0590-\u05FF]/); //check if there is hebrew characters in this string
        if(userName == ""){
            alert("אנא בחר שם משתמש")
            return
        }
        else if(password==""){
            alert("אנא בחר סיסמא")
            return
        }
        else if (position >=0) {
            alert("אנא בחר שם משתמש באותיות אנגליות בלבד")
            return
        }
        var authenticationRequest= {
            UserName: userName,
            Password: password
        };
        fetch('http://localhost/api/UserProfile/Register', {
            method: 'POST',
            headers: {
                'Content-Type':'application/json',
            }, 
            body: JSON.stringify(authenticationRequest)
    }).
    then(response => response.json()).
    then(authenticationResponse => {
        console.log(authenticationResponse.authenticationResult);
        if(authenticationResponse.authenticationResult == "Success"){
            localStorage.setItem("userName", authenticationRequest.UserName);
            window.location.href= "http://localhost/personalArea.html";
        }
        else{

            alert("משתמש כבר קיים");
        }
    }).
    catch(err => console.log(err));
}  
    catch(err) {
        alert(err);
    }
})