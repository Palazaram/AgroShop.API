using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AgroShop.Core.ValueObjects
{
    public class Sku : ValueObject
    {
        public string Value { get; }

        protected static int minLength = 4;
        protected static int maxLength = 20;

        private Sku(string value)
        {
            Value = value;
        }

        public static Result<Sku, Error> Create(string sku)
        {
            if (string.IsNullOrWhiteSpace(sku))
                return Result.Failure<Sku, Error>(Errors.Sku.SkuCantBeEmpty());

            // Normalized to a single case so "abc-1" and "ABC-1" can't sneak in
            // as two different-looking SKUs referring to the same product.
            sku = sku.Trim().ToUpperInvariant();

            if (sku.Length < minLength)
                return Result.Failure<Sku, Error>(Errors.Sku.SkuInvalidMinLength(minLength));

            if (sku.Length > maxLength)
                return Result.Failure<Sku, Error>(Errors.Sku.SkuInvalidMaxLength(maxLength));

            if (!Regex.IsMatch(sku, @"^[A-Z0-9_-]+$"))
                return Result.Failure<Sku, Error>(Errors.Sku.SkuInvalidFormat());

            return Result.Success<Sku, Error>(new Sku(sku));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
