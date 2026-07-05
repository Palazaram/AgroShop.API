using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AgroShop.Core.ValueObjects
{
    public class LastName : ValueObject
    {
        public string Value { get; }

        protected static int minLength = 2;
        protected static int maxLength = 50;

        private LastName(string value)
        {
            Value = value;
        }

        public static Result<LastName, Error> Create(string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                return Result.Failure<LastName, Error>(Errors.LastName.LastNameCantBeEmpty());

            lastName = lastName.Trim();

            if (lastName.Length < minLength)
                return Result.Failure<LastName, Error>(Errors.LastName.LastNameInvalidMinLength());

            if (lastName.Length > maxLength)
                return Result.Failure<LastName, Error>(Errors.LastName.LastNameInvalidMaxLength());

            if (!Regex.IsMatch(lastName, @"^[A-Za-zА-Яа-яІіЇїЄєҐґ'\-\s]+$"))
                return Result.Failure<LastName, Error>(Errors.LastName.LastNameInvalidFormat());

            if (!Regex.IsMatch(lastName, @"^[А-ЯІЇЄҐа-яіїєґ'\-\s]+$"))
                return Result.Failure<LastName, Error>(Errors.LastName.LastNameInvalidLanguage());

            return Result.Success<LastName, Error>(new LastName(lastName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
