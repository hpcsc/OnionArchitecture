using AutoMapper;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Services.Interfaces.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO;
using System;
using System.Linq;

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

        public CustomPrincipalSerializationModel GetUserSerializationModelByUsername(string username)
        {
            var user = _userRepository.FindBy(u => u.UserName == username).FirstOrDefault();
            if(user == null)
            {
                throw new ApplicationException(string.Format("User with username '{0}' not found", username));
            }

            return Mapper.Map<User, CustomPrincipalSerializationModel>(user);
        }
    }
}