using AgroShop.Application.Dto.SupplierDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.Application.Validators.SupplierValidators
{
    public class AddSupplierDtoValidator : AbstractValidator<AddSupplierDto>
    {
        public AddSupplierDtoValidator()
        {
            RuleFor(x => x.Name).MustBeValueObject(SupplierName.Create);
        }
    }
}
