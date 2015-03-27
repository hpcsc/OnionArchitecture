
namespace OnionArchitecture.Services.Interfaces.Common
{
    public interface IAuthenticateService
    {
        bool IsValidUser(string username, string password);
    }
}
