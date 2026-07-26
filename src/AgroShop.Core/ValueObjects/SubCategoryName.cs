using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AgroShop.Core.ValueObjects
{
    public class SubCategoryName : ValueObject
    {
        public string Value { get; }

        protected static int minLength = 2;
        protected static int maxLength = 50;

        private SubCategoryName(string value)
        {
            Value = value;
        }

        public static Result<SubCategoryName, Error> Create(string subCategoryName)
        {
            if (string.IsNullOrWhiteSpace(subCategoryName))
                return Result.Failure<SubCategoryName, Error>(Errors.SubCategoryName.SubCategoryNameCantBeEmpty());

            subCategoryName = subCategoryName.Trim();

            if (subCategoryName.Length < minLength)
                return Result.Failure<SubCategoryName, Error>(Errors.SubCategoryName.SubCategoryNameInvalidMinLength(minLength));

            if (subCategoryName.Length > maxLength)
                return Result.Failure<SubCategoryName, Error>(Errors.SubCategoryName.SubCategoryNameInvalidMaxLength(maxLength));

            if (!Regex.IsMatch(subCategoryName, @"^[A-Za-zА-Яа-яІіЇїЄєҐґ'\-\s]+$"))
                return Result.Failure<SubCategoryName, Error>(Errors.SubCategoryName.SubCategoryNameInvalidFormat());

            if (!Regex.IsMatch(subCategoryName, @"^[А-ЯІЇЄҐа-яіїєґ'\-\s]+$"))
                return Result.Failure<SubCategoryName, Error>(Errors.SubCategoryName.SubCategoryNameInvalidLanguage());

            return Result.Success<SubCategoryName, Error>(new SubCategoryName(subCategoryName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
