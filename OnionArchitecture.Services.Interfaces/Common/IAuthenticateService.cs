
using OnionArchitecture.Services.Interfaces.Common.DTO;
namespace OnionArchitecture.Services.Interfaces.Common
{
    public interface IAuthenticateService
    {
        bool IsValidUser(string username, string password);
        CustomPrincipalSerializationModel GetUserSerializationModelByUsername(string username);
    }
}
