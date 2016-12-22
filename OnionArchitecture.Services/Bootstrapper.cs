using Autofac;
using AutoMapper;
using FluentValidation;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Services.Common;
using OnionArchitecture.Services.Common.Validators;
using OnionArchitecture.Services.Interfaces.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;

namespace OnionArchitecture.Services
{
    public class Bootstrapper
    {
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<ManagePermissionService>().As<IManagePermissionService>().InstancePerRequest()
                    .PropertiesAutowired();
            builder.RegisterType<AuthenticateService>().As<IAuthenticateService>().InstancePerRequest();

            builder.RegisterType<UpdateUserRolesAndPermissionValidator>().
                As<IValidator<UpdateUserRolesAndPermissionInputModel>>().InstancePerRequest();
            builder.RegisterType<UpdateResourceValidator>().
                As<IValidator<UpdateResourceInputModel>>().InstancePerRequest();
            builder.RegisterType<AddResourceValidator>().
                As<IValidator<AddResourceInputModel>>().InstancePerRequest();
        }

        public void Configure()
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
