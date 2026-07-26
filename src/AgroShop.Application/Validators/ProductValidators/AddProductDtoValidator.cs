using AgroShop.Application.Dto.ProductDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.Application.Validators.ProductValidators
{
    public class AddProductDtoValidator : AbstractValidator<AddProductDto>
    {
        public AddProductDtoValidator()
        {
            RuleFor(x => x.Name).MustBeValueObject(ProductName.Create);
            RuleFor(x => x.Description).MustBeValueObject(ProductDescription.Create);
            RuleFor(x => x.Price).MustBeValueObject(Money.Create);
            RuleFor(x => x.Sku).MustBeValueObject(Sku.Create);
            RuleFor(x => x.StockQuantity).MustBeValueObject(StockQuantity.Create);
            RuleFor(x => x.PackageAmount)
                .GreaterThan(0)
                .WithMessage(Errors.PackageSize.PackageAmountMustBePositive().Serialize());
            RuleFor(x => x.SubCategoryId).NotEmptyCustom();
            RuleFor(x => x.SupplierId).NotEmptyCustom();
            RuleFor(x => x.Image)
                .NotNull()
                .WithMessage(Errors.General.ValueIsRequired("Зображення").Serialize())
                .MustBeValidImage();
        }
    }
}
