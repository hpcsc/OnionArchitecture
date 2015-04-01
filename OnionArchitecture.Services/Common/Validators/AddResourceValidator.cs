using FluentValidation;
using FluentValidation.Results;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;
using System.Linq;

namespace OnionArchitecture.Services.Common.Validators
{
    public class AddResourceValidator : AbstractValidator<AddResourceInputModel>
    {
        public AddResourceValidator(IResourceRepository resourceRepository)
        {
            RuleFor(c => c.Name).NotEmpty();
            Custom((input, context) =>
                {
                    if (resourceRepository.FindBy(r => r.Name == input.Name && r.ParentId == input.ParentId).FirstOrDefault() != null)
                    {
                        return new ValidationFailure("Resource", string.Format("Another resource with name '{0}' at the same level exists", input.Name));
                    }

                    return null;
                });
        }
    }
}
