using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AgroShop.Core.ValueObjects
{
    public class AttributeName : ValueObject
    {
        public string Value { get; }

        protected static int minLength = 2;
        protected static int maxLength = 50;

        private AttributeName(string value)
        {
            Value = value;
        }

        public static Result<AttributeName, Error> Create(string attributeName)
        {
            if (string.IsNullOrWhiteSpace(attributeName))
                return Result.Failure<AttributeName, Error>(Errors.AttributeName.AttributeNameCantBeEmpty());

            attributeName = attributeName.Trim();

            if (attributeName.Length < minLength)
                return Result.Failure<AttributeName, Error>(Errors.AttributeName.AttributeNameInvalidMinLength(minLength));

            if (attributeName.Length > maxLength)
                return Result.Failure<AttributeName, Error>(Errors.AttributeName.AttributeNameInvalidMaxLength(maxLength));

            if (!Regex.IsMatch(attributeName, @"^[A-Za-zА-Яа-яІіЇїЄєҐґ'\-\s]+$"))
                return Result.Failure<AttributeName, Error>(Errors.AttributeName.AttributeNameInvalidFormat());

            if (!Regex.IsMatch(attributeName, @"^[А-ЯІЇЄҐа-яіїєґ'\-\s]+$"))
                return Result.Failure<AttributeName, Error>(Errors.AttributeName.AttributeNameInvalidLanguage());

            return Result.Success<AttributeName, Error>(new AttributeName(attributeName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
