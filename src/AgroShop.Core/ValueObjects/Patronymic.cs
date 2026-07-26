using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AgroShop.Core.ValueObjects
{
    public class Patronymic : ValueObject
    {
        public string Value { get; }

        protected static int minLength = 2;
        protected static int maxLength = 50;

        private Patronymic(string value)
        {
            Value = value;
        }

        public static Result<Patronymic, Error> Create(string? patronymic)
        {
            if (string.IsNullOrWhiteSpace(patronymic))
                return Result.Failure<Patronymic, Error>(Errors.Patronymic.PatronymicCantBeEmpty());

            patronymic = patronymic.Trim();

            if (patronymic.Length < minLength)
                return Result.Failure<Patronymic, Error>(Errors.Patronymic.PatronymicInvalidMinLength(minLength));

            if (patronymic.Length > maxLength)
                return Result.Failure<Patronymic, Error>(Errors.Patronymic.PatronymicInvalidMaxLength(maxLength));

            if (!Regex.IsMatch(patronymic, @"^[A-Za-zА-Яа-яІіЇїЄєҐґ'\-\s]+$"))
                return Result.Failure<Patronymic, Error>(Errors.Patronymic.PatronymicInvalidFormat());

            if (!Regex.IsMatch(patronymic, @"^[А-ЯІЇЄҐа-яіїєґ'\-\s]+$"))
                return Result.Failure<Patronymic, Error>(Errors.Patronymic.PatronymicInvalidLanguage());

            return Result.Success<Patronymic, Error>(new Patronymic(patronymic));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
