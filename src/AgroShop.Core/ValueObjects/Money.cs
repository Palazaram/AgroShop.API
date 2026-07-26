using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.ValueObjects
{
    public class Money : ValueObject
    {
        public decimal Value { get; }

        private Money(decimal value)
        {
            Value = value;
        }

        public static Result<Money, Error> Create(decimal amount)
        {
            if (amount <= 0)
                return Result.Failure<Money, Error>(Errors.Money.MoneyMustBePositive());

            if (decimal.Round(amount, 2) != amount)
                return Result.Failure<Money, Error>(Errors.Money.MoneyInvalidPrecision());

            return Result.Success<Money, Error>(new Money(amount));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
