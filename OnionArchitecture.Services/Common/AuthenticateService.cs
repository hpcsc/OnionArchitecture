using System.Linq;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Services.Interfaces.Common;

namespace OnionArchitecture.Services.Common
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticateService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool IsValidUser(string username, string password)
        {
            var user = _userRepository.FindBy(u => u.UserName == username).FirstOrDefault();
            if (user == null)
            {
                return false;
            }

            return user.Password == password;
        }
    }
}