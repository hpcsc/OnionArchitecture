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
        private IResourceRepository _resourceRepository;

        public ManagePermissionService(IRoleRepository roleRepository, 
                                       IUserRepository userRepository, 
                                       IResourceRepository resourceRepository,
                                       IValidatorFactory validatorFactory)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;
            _validatorFactory = validatorFactory;
        }

        public PermissionIndexModel CreateIndexModel()
        {
            var model = new PermissionIndexModel
            {
                Users = _userRepository.FindAll().Select(Mapper.Map<User, UserDTO>),
                Resources = _resourceRepository.FindAll().Select(Mapper.Map<Resource, ResourceDTO>)
            };

            return model;
        }

        public DisplayUserPermissionModel GetUserPermission(string username)
        {
            var user = _userRepository.FindBy(u => u.UserName == username, 
                u => u.Roles, 
                u => u.Permissions.Select(p => p.Resource)).FirstOrDefault();

            var model = new DisplayUserPermissionModel
            {
                FullName = user.FullName,
                Roles = user.Roles.Select(Mapper.Map<Role, RoleDTO>).ToList(),
                Permissions = user.Permissions.Select(Mapper.Map<Permission, PermissionDTO>).ToList()
            };

            return model;
        }

        public void UpdateUserRolesAndPermission(UpdateUserRolesAndPermissionInputModel input)
        {
            var validator = _validatorFactory.GetValidator<UpdateUserRolesAndPermissionInputModel>();
            validator.ValidateAndThrow(input);

            //Update
        }
    }
}
