using AgroShop.Core.Enums;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.ValueObjects
{
    public class PackageSize : ValueObject
    {
        public decimal Amount { get; }
        public PackageUnit Unit { get; }

        private PackageSize(decimal amount, PackageUnit unit)
        {
            Amount = amount;
            Unit = unit;
        }

        public static Result<PackageSize, Error> Create(decimal amount, PackageUnit unit)
        {
            if (amount <= 0)
                return Result.Failure<PackageSize, Error>(Errors.PackageSize.PackageAmountMustBePositive());

            return Result.Success<PackageSize, Error>(new PackageSize(amount, unit));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Unit;
        }
    }
}
