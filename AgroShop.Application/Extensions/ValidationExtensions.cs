using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace AgroShop.Application.Extensions
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> NotEmptyCustom<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return DefaultValidatorExtensions.NotEmpty(ruleBuilder)
                .WithMessage(Errors.General.ValueIsRequired().Serialize());
        }

        public static IRuleBuilderOptions<T, string> MaximumLengthCustom<T>(this IRuleBuilder<T, string> ruleBuilder, int maximumLength)
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
