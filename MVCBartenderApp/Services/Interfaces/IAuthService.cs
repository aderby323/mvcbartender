using MVCBartenderApp.Models;
using MVCBartenderApp.Models.ViewModels;

namespace MVCBartenderApp.Services.Interfaces
{
    public interface IAuthService
    {
        string HashPassword(string password, string salt);
        string GenerateSalt();
        User ValidateLogin(LoginViewModel login);
    }
}
