using AutoMapper;
using FluentValidation;
using OnionArchitecture.Core.Infrastructure.Repositories;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Services.Interfaces.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;
using System.Linq;

namespace OnionArchitecture.Services.Common
{
    public class ManagePermissionService : IManagePermissionService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IValidatorFactory _validatorFactory;
        private readonly IResourceRepository _resourceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ManagePermissionService(IRoleRepository roleRepository, 
                                       IUserRepository userRepository, 
                                       IResourceRepository resourceRepository,
                                       IValidatorFactory validatorFactory,
                                       IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;
            _validatorFactory = validatorFactory;
            _unitOfWork = unitOfWork;
        }

        public PermissionIndexModel CreateIndexModel()
        {
            var resources = _resourceRepository.FindBy(r => !r.ParentId.HasValue, r => r.Children)
                                               .Select(Mapper.Map<Resource, ResourceDTO>)
                                               .OrderBy(r => r.Name);

            var model = new PermissionIndexModel
            {
                Users = _userRepository.FindAll().Select(Mapper.Map<User, UserDTO>),
                Resources = _resourceRepository.FindBy(r => !r.ParentId.HasValue)
                                               .Select(Mapper.Map<Resource, ResourceDTO>)
                                               .OrderBy(r => r.Name)
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

        public DisplayResourceDetailModel GetResourceDetail(int resourceId)
        {
            var users = _userRepository.FindAll();
            var roles = _roleRepository.FindAll();
            var resource = _resourceRepository.FindBy(r => r.Id == resourceId, 
                                                      r => r.Permissions.Select(p => p.User), 
                                                      r => r.Permissions.Select(p => p.Role)).FirstOrDefault();

            var model = new DisplayResourceDetailModel
            {
                ResourceId = resource.Id,
                ResourceName = resource.Name,
                ResourceDescription = resource.Description,
                Permissions = resource.Permissions.Select(p => 
                    {
                        if(p.UserId.HasValue)
                        {
                            return new DisplayResourceDetailPermissionModel
                            {
                                Id = p.UserId.Value,
                                Name = p.User.FullName,
                                Type = "User",
                                Permission = p.Type
                            };
                        }
                        else
                        {
                            return new DisplayResourceDetailPermissionModel
                            {
                                Id = p.RoleId.Value,
                                Name = p.Role.Name,
                                Type = "Role",
                                Permission = p.Type
                            };
                        }
                    }).ToList()
            };

            var addedRoleIds = model.Permissions.Where(r => r.Type == "Role").Select(p => p.Id).Distinct();
            var addedUserIds = model.Permissions.Where(r => r.Type == "User").Select(p => p.Id).Distinct();

            model.Permissions.AddRange(roles.Where(r => !addedRoleIds.Contains(r.Id)).Select(r => new DisplayResourceDetailPermissionModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Type = "Role",
                    Permission = PermissionType.None
                }));

            model.Permissions.AddRange(users.Where(r => !addedUserIds.Contains(r.Id)).Select(r => new DisplayResourceDetailPermissionModel
            {
                Id = r.Id,
                Name = r.FullName,
                Type = "User",
                Permission = PermissionType.None
            }));

            model.Permissions = model.Permissions.OrderBy(p => p.Type).ThenBy(p => p.Name).ToList();

            return model;
        }

        public void UpdateUserRolesAndPermission(UpdateUserRolesAndPermissionInputModel input)
        {
            var validator = _validatorFactory.GetValidator<UpdateUserRolesAndPermissionInputModel>();
            validator.ValidateAndThrow(input);
            
            //Update
        }

        public void UpdateResource(UpdateResourceInputModel input)
        {
            var validator = _validatorFactory.GetValidator<UpdateResourceInputModel>();
            validator.ValidateAndThrow(input);

            var resourceToUpdate = _resourceRepository.FindBy(input.Id);
            resourceToUpdate.Name = input.Name;
            resourceToUpdate.Description = input.Description;

            _resourceRepository.Update(resourceToUpdate);
            _unitOfWork.Commit();
        }
    }
}
