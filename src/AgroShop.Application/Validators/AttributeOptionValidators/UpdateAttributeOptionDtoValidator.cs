using AgroShop.Application.Dto.AttributeOptionDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.Application.Validators.AttributeOptionValidators
{
    public class UpdateAttributeOptionDtoValidator : AbstractValidator<UpdateAttributeOptionDto>
    {
        public UpdateAttributeOptionDtoValidator()
        {
            RuleFor(x => x.Value).MustBeValueObject(AttributeOptionValue.Create);
        }
    }
}
