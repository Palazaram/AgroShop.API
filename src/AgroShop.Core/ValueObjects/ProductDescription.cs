using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.ValueObjects
{
    public class ProductDescription : ValueObject
    {
        public string Value { get; }

        protected static int minLength = 20;
        protected static int maxLength = 400;

        private ProductDescription(string value)
        {
            Value = value;
        }

        public static Result<ProductDescription, Error> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return Result.Failure<ProductDescription, Error>(Errors.ProductDescription.ProductDescriptionCantBeEmpty());

            description = description.Trim();

            if (description.Length < minLength)
                return Result.Failure<ProductDescription, Error>(Errors.ProductDescription.ProductDescriptionInvalidMinLength(minLength));

            if (description.Length > maxLength)
                return Result.Failure<ProductDescription, Error>(Errors.ProductDescription.ProductDescriptionInvalidMaxLength(maxLength));

            return Result.Success<ProductDescription, Error>(new ProductDescription(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
