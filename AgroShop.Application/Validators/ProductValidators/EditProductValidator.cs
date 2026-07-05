using AgroShop.Application.Dto.ProductDto;
using FluentValidation;

namespace AgroShop.Application.Validators.ProductValidators
{
    public class EditProductValidator : AbstractValidator<EditProductDto>
    {
        public EditProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(Guid.Empty).WithMessage("ID продукту є обов’язковим.");

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

            // 1. Если нет текущего изображения (ImagePath пустой) — требуем новое
            RuleFor(x => x.Image)
                .NotNull()
                .When(x => string.IsNullOrWhiteSpace(x.ImagePath))
                .WithMessage("Зображення є обов’язковим для продукту, що ще не має фото.");

            // 2. Если передали файл — он не должен быть пустым
            RuleFor(x => x.Image)
                .Must(file => file == null || file.Length > 0)
                .WithMessage("Файл зображення не може бути порожнім.");

            // 3. Если передали файл — проверяем формат (jpg/png)
            RuleFor(x => x.Image)
                .Must(file =>
                {
                    if (file == null) return true;
                    var allowedTypes = new[] { "image/jpeg", "image/png" };
                    return allowedTypes.Contains(file.ContentType.ToLower());
                })
                .WithMessage("Підтримуються лише зображення формату JPG або PNG.");
        }
    }
}
