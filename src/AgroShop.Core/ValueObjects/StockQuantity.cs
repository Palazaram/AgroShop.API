using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.ValueObjects
{
    public class StockQuantity : ValueObject
    {
        public int Value { get; }

        private StockQuantity(int value)
        {
            Value = value;
        }

        public static Result<StockQuantity, Error> Create(int quantity)
        {
            if (quantity < 0)
                return Result.Failure<StockQuantity, Error>(Errors.StockQuantity.StockQuantityCantBeNegative());

            return Result.Success<StockQuantity, Error>(new StockQuantity(quantity));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
