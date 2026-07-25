using AgroShop.Application.Dto.SubCategoryDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.Application.Validators.SubCategoryValidators
{
    public class AddSubCategoryDtoValidator : AbstractValidator<AddSubCategoryDto>
    {
        public AddSubCategoryDtoValidator()
        {
            RuleFor(x => x.Name).MustBeValueObject(SubCategoryName.Create);
            RuleFor(x => x.CategoryId).NotEmptyCustom();
        }
    }
}
