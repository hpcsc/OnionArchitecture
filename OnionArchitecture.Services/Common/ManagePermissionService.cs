using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Services.Interfaces.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;

namespace OnionArchitecture.Services.Common
{
    public class ManagePermissionService : IManagePermissionService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IValidatorFactory _validatorFactory;

        public ManagePermissionService(IRoleRepository roleRepository, 
                                       IUserRepository userRepository, 
                                       IValidatorFactory validatorFactory)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _validatorFactory = validatorFactory;
        }

        public IEnumerable<UserDTO> FindAllUsers()
        {
            return _userRepository.FindAll().Select(Mapper.Map<User, UserDTO>);
        }

        public void UpdateUserRolesAndPermission(UpdateUserRolesAndPermissionInputModel input)
        {
            var validator = _validatorFactory.GetValidator<UpdateUserRolesAndPermissionInputModel>();
            validator.ValidateAndThrow(input);

            //Update
        }
    }
}
