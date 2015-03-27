using Autofac;
using FluentValidation;
using OnionArchitecture.Services.Common;
using OnionArchitecture.Services.Common.Validators;
using OnionArchitecture.Services.Interfaces.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;

namespace OnionArchitecture.Bootstrapper.DependencyResolution
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ManagePermissionService>().As<IManagePermissionService>().InstancePerRequest();
            builder.RegisterType<AuthenticateService>().As<IAuthenticateService>().InstancePerRequest();

            builder.RegisterType<UpdateUserRolesAndPermissionValidator>().
                As<IValidator<UpdateUserRolesAndPermissionInputModel>>().InstancePerRequest();
        }
    }
}
