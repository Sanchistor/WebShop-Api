namespace WebShop.WebShop.Core.Auth
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string email, string password);
    }
}
