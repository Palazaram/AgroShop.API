using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AgroShop.Core.ValueObjects
{
    public class Login : ValueObject
    {
        public string Value { get; }

        protected static int minLength = 3;
        protected static int maxLength = 100;

        private Login(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static Result<Login, Error> Create(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return Result.Failure<Login, Error>(Errors.Login.LoginCantBeEmpty());

            login = login.Trim();

            if (login.Length < minLength)
                return Result.Failure<Login, Error>(Errors.Login.LoginInvalidMinLength(minLength));

            if (login.Length > maxLength)
                return Result.Failure<Login, Error>(Errors.Login.LoginInvalidMaxLength(maxLength));

            if (!IsValidLogin(login))
                return Result.Failure<Login, Error>(Errors.Login.LoginInvalidFormat());

            return Result.Success<Login, Error>(new Login(login));
        }

        public static implicit operator string(Login login)
        {
            return login.Value;
        }

        public override string ToString()
        {
            return Value;
        }

        private static bool IsValidLogin(string login)
        {
            // Только английские буквы, цифры и подчеркивание
            var regex = new Regex(@"^[a-zA-Z0-9_]+$", RegexOptions.Compiled);
            return regex.IsMatch(login);
        }
    }
}
