using AutoMapper;
using FluentValidation;
using OnionArchitecture.Core.Infrastructure.Repositories;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Services.Interfaces.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;
using System.Collections.Generic;
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
        private readonly IPermissionRepository _permissionRepository;

        public ManagePermissionService(IRoleRepository roleRepository, 
                                       IUserRepository userRepository, 
                                       IResourceRepository resourceRepository,
                                       IPermissionRepository permissionRepository,
                                       IValidatorFactory validatorFactory,
                                       IUnitOfWork unitOfWork)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _resourceRepository = resourceRepository;
            _permissionRepository = permissionRepository;
            _validatorFactory = validatorFactory;
            _unitOfWork = unitOfWork;
        }

        public PermissionIndexModel CreateIndexModel()
        {
            var hierarchy = _resourceRepository.GetResourceHierarchy();

            var model = new PermissionIndexModel
            {
                Resources = hierarchy
                                    .Select(Mapper.Map<Resource, ResourceDTO>)
                                    .OrderBy(r => r.Name),
                AvailableRoles = _roleRepository.FindAll().Select(Mapper.Map<Role, RoleDTO>)
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
                Permissions = user.Permissions.Select(Mapper.Map<Permission, PermissionDTO>)
                                              .OrderBy(p => p.ResourceName)
                                              .ToList()
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
                                PermissionId = p.Id,
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
                                PermissionId = p.Id,
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

            var newPermissions = input.Permissions.Where(i => i.PermissionId == 0).Select(i => new Permission
                {
                    UserId = i.IsRole ? null : (int?)i.Id,
                    RoleId = i.IsRole ? (int?)i.Id : null,
                    ResourceId = input.Id,
                    Type = i.Permission
                });

            foreach (var newPermission in newPermissions)
            {
                _permissionRepository.Add(newPermission);    
            }

            var existingPermissionIds = input.Permissions.Where(p => p.PermissionId > 0).Select(p => p.PermissionId);
            var existingPermissions = _permissionRepository.FindBy(p => existingPermissionIds.Contains(p.Id));
            foreach (var existingPermission in existingPermissions)
            {
                var i = input.Permissions.FirstOrDefault(p => p.PermissionId == existingPermission.Id);
                if(i != null)
                {
                    existingPermission.Type = i.Permission;
                    _permissionRepository.Update(existingPermission);
                }
            }

            _unitOfWork.Commit();
        }

        public void AddResource(AddResourceInputModel input)
        {
            var validator = _validatorFactory.GetValidator<AddResourceInputModel>();
            validator.ValidateAndThrow(input);

            _resourceRepository.Add(new Resource
                {
                    Name = input.Name,
                    ParentId = input.ParentId
                });

            _unitOfWork.Commit();
        }

        public IEnumerable<UserDTO> SearchUser(string input)
        {
            var users = string.IsNullOrWhiteSpace(input) ?
                _userRepository.FindAll() :
                _userRepository.FindBy(u => u.UserName.Contains(input));

            return users.Select(Mapper.Map<User, UserDTO>);
        }
    }
}
