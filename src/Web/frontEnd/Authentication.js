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
            alert(authenticationResponse.authenticationResult);
            window.location.href= "http://localhost/Authentication.html";
            //document.getElementById('loginError').innerText = authenticationResponse.authenticationResult;
        }
    }).
    catch(err => console.log(err));
}  
    catch(err) {
        alert(err);
    }
})
