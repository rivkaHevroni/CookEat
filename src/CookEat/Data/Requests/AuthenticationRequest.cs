namespace CookEat
{
    public class AuthenticationRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticationResponse
    {
        public AuthenticationResult AuthenticationResult { get; set; }
    }

    public enum AuthenticationResult
    {
        Success,
        UserDoesNotExist,
        UserAlreadyExists,
        IncorrectPassword
    }
}