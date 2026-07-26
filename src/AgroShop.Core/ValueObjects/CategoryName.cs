using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AgroShop.Core.ValueObjects
{
    public class CategoryName : ValueObject
    {
        public string Value { get; }

        protected static int minLength = 2;
        protected static int maxLength = 50;

        private CategoryName(string value)
        {
            Value = value;
        }

        public static Result<CategoryName, Error> Create(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
                return Result.Failure<CategoryName, Error>(Errors.CategoryName.CategoryNameCantBeEmpty());

            categoryName = categoryName.Trim();

            if (categoryName.Length < minLength)
                return Result.Failure<CategoryName, Error>(Errors.CategoryName.CategoryNameInvalidMinLength(minLength));

            if (categoryName.Length > maxLength)
                return Result.Failure<CategoryName, Error>(Errors.CategoryName.CategoryNameInvalidMaxLength(maxLength));

            if (!Regex.IsMatch(categoryName, @"^[A-Za-zА-Яа-яІіЇїЄєҐґ'\-\s]+$"))
                return Result.Failure<CategoryName, Error>(Errors.CategoryName.CategoryNameInvalidFormat());

            if (!Regex.IsMatch(categoryName, @"^[А-ЯІЇЄҐа-яіїєґ'\-\s]+$"))
                return Result.Failure<CategoryName, Error>(Errors.CategoryName.CategoryNameInvalidLanguage());

            return Result.Success<CategoryName, Error>(new CategoryName(categoryName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
