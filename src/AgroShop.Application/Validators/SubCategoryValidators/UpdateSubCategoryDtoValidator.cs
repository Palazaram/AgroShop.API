using AgroShop.Application.Dto.SubCategoryDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.Application.Validators.SubCategoryValidators
{
    public class UpdateSubCategoryDtoValidator : AbstractValidator<UpdateSubCategoryDto>
    {
        public UpdateSubCategoryDtoValidator()
        {
            RuleFor(x => x.Name).MustBeValueObject(SubCategoryName.Create);
            RuleFor(x => x.CategoryId).NotEmptyCustom();
        }
    }
}
