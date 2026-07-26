using AgroShop.Application.Dto.AttributeOptionDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.Application.Validators.AttributeOptionValidators
{
    public class AddAttributeOptionDtoValidator : AbstractValidator<AddAttributeOptionDto>
    {
        public AddAttributeOptionDtoValidator()
        {
            RuleFor(x => x.Value).MustBeValueObject(AttributeOptionValue.Create);
            RuleFor(x => x.AttributeId).NotEmptyCustom();
        }
    }
}
