using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Services.Interfaces.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO;

namespace OnionArchitecture.Services.Common
{
    public class ManagePermissionService : IManagePermissionService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        public ManagePermissionService(IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<UserDTO> FindAllUsers()
        {
            return _userRepository.FindAll().Select(Mapper.Map<User, UserDTO>);
        }
    }
}
