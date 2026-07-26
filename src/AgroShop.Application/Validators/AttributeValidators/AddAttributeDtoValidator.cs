using AgroShop.Application.Dto.AttributeDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.Application.Validators.AttributeValidators
{
    public class AddAttributeDtoValidator : AbstractValidator<AddAttributeDto>
    {
        public AddAttributeDtoValidator()
        {
            RuleFor(x => x.Name).MustBeValueObject(AttributeName.Create);
            RuleFor(x => x.ValueType).IsInEnum();
        }
    }
}
