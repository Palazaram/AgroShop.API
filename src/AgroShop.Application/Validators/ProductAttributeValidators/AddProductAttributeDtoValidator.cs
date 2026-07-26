using AgroShop.Application.Dto.ProductAttributeDto;
using AgroShop.Application.Extensions;
using FluentValidation;

namespace AgroShop.Application.Validators.ProductAttributeValidators
{
    public class AddProductAttributeDtoValidator : AbstractValidator<AddProductAttributeDto>
    {
        public AddProductAttributeDtoValidator()
        {
            RuleFor(x => x.SubCategoryId).NotEmptyCustom();
            RuleFor(x => x.AttributeId).NotEmptyCustom();
        }
    }
}
