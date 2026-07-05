using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AgroShop.Core.ValueObjects
{
    public class Email : ValueObject
    {
        public string? Value { get; }

        protected static int maxLength = 256;

        private Email(string? value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value?.ToLowerInvariant() ?? string.Empty;
        }

        public static Result<Email, Error> Create(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return Result.Success<Email, Error>(new Email(null));

            email = email.Trim().ToLowerInvariant();

            if (email.Length > maxLength)
                return Result.Failure<Email, Error>(Errors.Email.EmailInvalidMaxLength(maxLength));

            if (!Regex.IsMatch(email, @"^(?!\.)(?!.*\.\.)[a-z0-9_'+\-\.]*[a-z0-9_+\-]@([a-z0-9][a-z0-9\-]*\.)+[a-z]{2,}$", RegexOptions.IgnoreCase))
                return Result.Failure<Email, Error>(Errors.Email.EmailInvalidFormat());

            return Result.Success<Email, Error>(new Email(email));
        }
    }
}
