using AutoMapper;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO;

namespace OnionArchitecture.Bootstrapper
{
    public class AutoMapperConfig
    {
        public static void SetupBindings()
        {
            Mapper.CreateMap<Role, RoleDTO>();
            Mapper.CreateMap<User, UserDTO>();
            Mapper.CreateMap<Permission, PermissionDTO>();
            Mapper.CreateMap<Resource, ResourceDTO>();
        }
    }
}
