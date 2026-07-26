using AgroShop.Application.Dto.ProductDto;
using AgroShop.Application.Extensions;
using AgroShop.Core.ValueObjects;
using FluentValidation;

namespace AgroShop.Application.Validators.ProductValidators
{
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.Name).MustBeValueObject(ProductName.Create);
            RuleFor(x => x.Description).MustBeValueObject(ProductDescription.Create);
            RuleFor(x => x.Price).MustBeValueObject(Money.Create);
            RuleFor(x => x.Sku).MustBeValueObject(Sku.Create);
            RuleFor(x => x.StockQuantity).MustBeValueObject(StockQuantity.Create);
            RuleFor(x => x.SubCategoryId).NotEmptyCustom();
            RuleFor(x => x.SupplierId).NotEmptyCustom();
            RuleFor(x => x.Image).MustBeValidImage();
        }
    }
}
