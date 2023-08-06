namespace WebShop.WebShop.Core.Services.Authentication
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string email, string password);
    }
}
