using AgroShop.Application.Dto.CategoryDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.Application.Validators.CategoryValidators
{
    public class AddCategoryDtoValidator : AbstractValidator<AddCategoryDto>
    {
        public AddCategoryDtoValidator()
        {
            RuleFor(x => x.Name).MustBeValueObject(CategoryName.Create);
            RuleFor(x => x.Image)
                .NotNull()
                .WithMessage(Errors.General.ValueIsRequired("Зображення").Serialize())
                .MustBeValidImage();
        }
    }
}
