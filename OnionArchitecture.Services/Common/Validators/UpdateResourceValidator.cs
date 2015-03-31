using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using OnionArchitecture.Core.Models.Common;
using OnionArchitecture.Services.Interfaces.Common.DTO.Input;

namespace OnionArchitecture.Services.Common.Validators
{
    public class UpdateResourceValidator : AbstractValidator<UpdateResourceInputModel>
    {        
        public UpdateResourceValidator(IResourceRepository resourceRepository)
        {
            RuleFor(c => c.Name).NotEmpty();
            Custom((input, context) =>
            {
                var resourceFromDb = resourceRepository.FindBy(input.Id);
                if (resourceFromDb == null)
                {
                    return new ValidationFailure("Id", string.Format("Resouce with id {0} not found", input.Id));
                }

                if (resourceRepository.FindBy(r => r.Id != input.Id && 
                                                   r.ParentId == resourceFromDb.ParentId && 
                                                   r.Name == input.Name).FirstOrDefault() != null)
                {
                    return new ValidationFailure("Resource", string.Format("Another resource with name {0} at the same level exists", input.Name));
                }

                return null;
            });
        }
    }
}
