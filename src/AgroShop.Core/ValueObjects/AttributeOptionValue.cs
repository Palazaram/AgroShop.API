using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.ValueObjects
{
    // Deliberately no format/language regex, unlike the Name-type VOs - option
    // values are too varied in shape (weed names, timing phrases like "До
    // сходів", colors) to constrain the same way.
    public class AttributeOptionValue : ValueObject
    {
        public string Value { get; }

        protected static int maxLength = 100;

        private AttributeOptionValue(string value)
        {
            Value = value;
        }

        public static Result<AttributeOptionValue, Error> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<AttributeOptionValue, Error>(Errors.AttributeOption.AttributeOptionValueCantBeEmpty());

            value = value.Trim();

            if (value.Length > maxLength)
                return Result.Failure<AttributeOptionValue, Error>(Errors.AttributeOption.AttributeOptionValueInvalidMaxLength(maxLength));

            return Result.Success<AttributeOptionValue, Error>(new AttributeOptionValue(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
