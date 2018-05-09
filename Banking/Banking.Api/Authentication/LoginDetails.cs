namespace Banking.Api.Authentication
{
    public class LoginDetails
    {
        public LoginDetails(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        public string Password { get; }
    }
}