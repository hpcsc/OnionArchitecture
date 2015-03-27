using FluentValidation;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;

namespace OnionArchitecture.Services.Common.Validators
{
    public class UpdateUserRolesAndPermissionValidator : AbstractValidator<UpdateUserRolesAndPermissionInputModel>
    {
        public UpdateUserRolesAndPermissionValidator()
        {
            
        }
    }
}
