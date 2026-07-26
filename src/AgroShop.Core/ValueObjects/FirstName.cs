using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AgroShop.Core.ValueObjects
{
    public class FirstName : ValueObject
    {
        public string Value { get; }

        protected static int minLength = 2;
        protected static int maxLength = 50;

        private FirstName(string value)
        {
            Value = value;
        }

        public static Result<FirstName, Error> Create(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return Result.Failure<FirstName, Error>(Errors.FirstName.FirstNameCantBeEmpty());

            firstName = firstName.Trim();

            if (firstName.Length < minLength)
                return Result.Failure<FirstName, Error>(Errors.FirstName.FirstNameInvalidMinLength(minLength));

            if (firstName.Length > maxLength)
                return Result.Failure<FirstName, Error>(Errors.FirstName.FirstNameInvalidMaxLength(maxLength));

            if (!Regex.IsMatch(firstName, @"^[A-Za-zА-Яа-яІіЇїЄєҐґ'\-\s]+$"))
                return Result.Failure<FirstName, Error>(Errors.FirstName.FirstNameInvalidFormat());

            if (!Regex.IsMatch(firstName, @"^[А-ЯІЇЄҐа-яіїєґ'\-\s]+$"))
                return Result.Failure<FirstName, Error>(Errors.FirstName.FirstNameInvalidLanguage());

            return Result.Success<FirstName, Error>(new FirstName(firstName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
