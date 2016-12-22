using AutoMapper;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO;

namespace OnionArchitecture.Bootstrapper
{
    public class AutoMapperConfig
    {
        public static void SetupBindings()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<Role, RoleDTO>();
                config.CreateMap<User, UserDTO>();
                config.CreateMap<User, CustomPrincipalSerializationModel>();
                config.CreateMap<Permission, PermissionDTO>();
                config.CreateMap<Resource, ResourceDTO>();
            });
        }
    }
}
