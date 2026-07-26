using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace AgroShop.Application.Extensions
{
    public static class ValidationExtensions
    {
        private static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png", ".webp"];

        public static IRuleBuilderOptionsConditions<T, IFormFile?> MustBeValidImage<T>(
            this IRuleBuilder<T, IFormFile?> ruleBuilder,
            int maxMegabytes = 5)
        {
            return ruleBuilder.Custom((file, context) =>
            {
                if (file is null)
                    return;

                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!AllowedImageExtensions.Contains(extension))
                {
                    context.AddFailure(Errors.Image.InvalidFormat().Serialize());
                    return;
                }

                if (file.Length > maxMegabytes * 1024 * 1024)
                {
                    context.AddFailure(Errors.Image.InvalidSize(maxMegabytes).Serialize());
                }
            });
        }

        public static IRuleBuilderOptions<T, TProperty> NotEmptyCustom<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return DefaultValidatorExtensions.NotEmpty(ruleBuilder)
                .WithMessage(Errors.General.ValueIsRequired().Serialize());
        }

        public static IRuleBuilderOptions<T, string?> MaximumLengthCustom<T>(this IRuleBuilder<T, string?> ruleBuilder, int maximumLength)
        {
            return DefaultValidatorExtensions.MaximumLength(ruleBuilder, maximumLength)
                .WithMessage(Errors.General.InvalidLength().Serialize());
        }

        public static IRuleBuilderOptions<T, string> MustBeGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .Must(value => Guid.TryParse(value, out _))
                .WithMessage(Errors.General.IncorrectGuidError().Serialize());
        }

        public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TValueObject, Error>> factoryMethod) where TValueObject : ValueObject
        {
            return ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject, Error> result = factoryMethod(value);

                if (result.IsFailure)
                {
                    context.AddFailure(result.Error.Serialize());
                }
            });
        }
    }
}
