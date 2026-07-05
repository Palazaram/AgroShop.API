using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

public class Password : ValueObject
{
    public string Value { get; }

    protected static readonly int minLength = 8;
    protected static readonly int maxLength = 100;
    protected static readonly Regex AllowedCharactersRegex = 
        new Regex(@"^[A-Za-z0-9!@#$%^&*()\-_=+\[\]{};:,.<>/?~]+$", RegexOptions.Compiled);

    private Password(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value; // Пароль чувствителен к регистру
    }

    public static Result<Password, Error> Create(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return Result.Failure<Password, Error>(Errors.Password.PasswordCantBeEmpty());

        if (password.Length < minLength)
            return Result.Failure<Password, Error>(Errors.Password.PasswordInvalidMinLength(minLength));

        if (password.Length > maxLength)
            return Result.Failure<Password, Error>(Errors.Password.PasswordInvalidMaxLength(maxLength));

        if (!HasOnlyAllowedCharacters(password))
            return Result.Failure<Password, Error>(Errors.Password.PasswordInvalidFormat());

        if (!HasMinimumRequirements(password))
            return Result.Failure<Password, Error>(Errors.Password.PasswordLowDifficulty());

        return Result.Success<Password, Error>(new Password(password));
    }

    public override string ToString() => "***";

    private static bool HasMinimumRequirements(string password)
    {
        // Проверяем наличие хотя бы одной заглавной буквы, одной строчной и одной цифры
        var hasUpper = password.Any(char.IsUpper);
        //var hasLower = password.Any(char.IsLower);
        var hasDigit = password.Any(char.IsDigit);

        return hasUpper /*&& hasLower*/ && hasDigit;
    }

    private static bool HasOnlyAllowedCharacters(string password)
    {
        return AllowedCharactersRegex.IsMatch(password);
    }
}