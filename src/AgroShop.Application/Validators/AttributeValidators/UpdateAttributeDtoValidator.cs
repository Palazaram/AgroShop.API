using AgroShop.Application.Dto.AttributeDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.Application.Validators.AttributeValidators
{
    public class UpdateAttributeDtoValidator : AbstractValidator<UpdateAttributeDto>
    {
        public UpdateAttributeDtoValidator()
        {
            RuleFor(x => x.Name).MustBeValueObject(AttributeName.Create);
        }
    }
}
