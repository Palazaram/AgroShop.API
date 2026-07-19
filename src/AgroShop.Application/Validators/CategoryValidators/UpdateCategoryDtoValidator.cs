using AgroShop.Application.Dto.CategoryDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.Application.Validators.CategoryValidators
{
    public class UpdateCategoryDtoValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryDtoValidator()
        {
            RuleFor(x => x.Name).MustBeValueObject(CategoryName.Create);
            RuleFor(x => x.Image).MustBeValidImage();
        }
    }
}
