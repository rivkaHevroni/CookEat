document.getElementById("loginButton").addEventListener('click', (clickEvent) =>
{
    clickEvent.preventDefault();
    console.log("hey");
try {
        userName = document.getElementById('loginUserName').value;
        password = document.getElementById('loginPassword').value;
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