using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AgroShop.Core.ValueObjects
{
    public class ProductName : ValueObject
    {
        public string Value { get; }

        protected static int minLength = 2;
        protected static int maxLength = 50;

        private ProductName(string value)
        {
            Value = value;
        }

        public static Result<ProductName, Error> Create(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
                return Result.Failure<ProductName, Error>(Errors.ProductName.ProductNameCantBeEmpty());

            productName = productName.Trim();

            if (productName.Length < minLength)
                return Result.Failure<ProductName, Error>(Errors.ProductName.ProductNameInvalidMinLength(minLength));

            if (productName.Length > maxLength)
                return Result.Failure<ProductName, Error>(Errors.ProductName.ProductNameInvalidMaxLength(maxLength));

            if (!Regex.IsMatch(productName, @"^[A-Za-zА-Яа-яІіЇїЄєҐґ'\-\s]+$"))
                return Result.Failure<ProductName, Error>(Errors.ProductName.ProductNameInvalidFormat());

            if (!Regex.IsMatch(productName, @"^[А-ЯІЇЄҐа-яіїєґ'\-\s]+$"))
                return Result.Failure<ProductName, Error>(Errors.ProductName.ProductNameInvalidLanguage());

            return Result.Success<ProductName, Error>(new ProductName(productName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
