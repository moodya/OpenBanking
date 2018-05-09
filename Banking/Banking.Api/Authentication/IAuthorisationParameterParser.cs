namespace Banking.Api.Authentication
{
    public interface IAuthorisationParameterParser
    {
        LoginDetails Parse(string authorizationParameter, char separator = '|');
    }
}