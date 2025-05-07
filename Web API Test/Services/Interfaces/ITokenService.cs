using Web_API_Test.Models;

namespace Web_API_Test.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
