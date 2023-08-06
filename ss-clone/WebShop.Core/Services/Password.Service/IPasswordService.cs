namespace WebShop.WebShop.Core.Services.Password.Service
{
    public interface IPasswordService
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);
    }
}
