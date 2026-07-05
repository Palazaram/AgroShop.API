using AgroShop.Application.Dto.ProductDto;
using FluentValidation;

namespace AgroShop.Application.Validators.ProductValidators
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Назва є обов’язковою.")
                .MaximumLength(100).WithMessage("Назва не повинна перевищувати 100 символів.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Опис не повинен перевищувати 500 символів.");

            RuleFor(x => x.Price)
                .NotNull().WithMessage("Ціна є обов’язковою.")
                .GreaterThan(0).WithMessage("Ціна повинна бути більшою за нуль.");

            RuleFor(x => x.SubCategoryId)
                .NotNull().WithMessage("Підкатегорія є обов’язковою.")
                .NotEqual(Guid.Empty).WithMessage("Підкатегорія не може бути порожньою.");

            RuleFor(x => x.SupplierId)
                .NotNull().WithMessage("Постачальник є обов’язковим.")
                .NotEqual(Guid.Empty).WithMessage("Постачальник не може бути порожнім.");

            RuleFor(x => x.Image)
                .NotNull().WithMessage("Зображення є обов’язковим.")
                .Must(img => img == null || img.Length > 0).WithMessage("Файл зображення не може бути порожнім.")
                .Must(img =>
                {
                    if (img == null) return true; // NotNull is handled by a separate rule
                    var allowedTypes = new[] { "image/jpeg", "image/png" };
                    return allowedTypes.Contains(img.ContentType.ToLower());
                }).WithMessage("Підтримуються лише зображення формату JPG або PNG.");
        }
    }
}
