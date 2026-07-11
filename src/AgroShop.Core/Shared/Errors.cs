namespace AgroShop.Core.Shared
{
    public static class Errors
    {
        public static class General
        {
            public static Error ValueIsRequired(string? name = null)
            {
                var label = name ?? "Поле";
                return Error.Validation("value.is.required", $"{label} не може бути порожнім");
            }

            public static Error InvalidLength(string? name = null)
            {
                var label = name == null ? " " : " " + name + " ";
                return Error.Validation("invalid.string.length", $"Недопустима{label}довжина");
            }

            public static Error IncorrectGuidError(string message = "Некоректний Guid")
            {
                return Error.InternalServerError("incorrect.guid.error", message);
            }
        }

        public static class User 
        {
            public static Error UserIsNullById() => Error.Validation("user.is.null.by.id", "Користувача з даним ID не знайдено");
        }

        public static class Category
        {
            public static Error CategoryIsNullById() => Error.Validation("category.is.null.by.id", "Категорію з даним ID не знайдено");
        }

        public static class Email
        {
            public static Error EmailCantBeEmpty() => Error.Validation("email.cant.be.empty", "Електронна адреса є обов'язковою");
            public static Error EmailInvalidFormat() => Error.Validation("email.invalid.format", "Невірний формат електронної адреси");
            public static Error EmailInvalidMaxLength(int maxLength) => Error.Validation("email.invalid.max.length", $"Електронна адреса не має перевищувати {maxLength} символів");
        }

        public static class Password
        {
            public static Error PasswordCantBeEmpty() => Error.Validation("password.cant.be.empty", "Пароль є обов’язковим");
            public static Error PasswordLowDifficulty() => Error.Validation("password.low.difficulty", "Пароль повинен містити принаймні одну велику літеру та одну цифру");
            public static Error PasswordInvalidFormat() => Error.Validation("password.invalid.format", "Пароль може містити лише латинські літери, цифри та спецсимволи, без пробілів та кирилиці");
            public static Error PasswordInvalidMaxLength(int maxLength) => Error.Validation("password.invalid.max.length", $"Пароль не має перевищувати {maxLength} символів");
            public static Error PasswordInvalidMinLength(int minLength) => Error.Validation("password.invalid.min.length", $"Пароль повинен містити принаймні {minLength} символів");
        }

        public static class Phone
        {
            public static Error PhoneCantBeEmpty() => Error.Validation("phone.cant.be.empty", "Номер телефону є обов’язковим");
            public static Error PhoneInvalidLength() => Error.Validation("phone.invalid.length", $"Невірний формат номера телефону");        
            public static Error PhoneInvalidOperatorCode() => Error.Validation("phone.invalid.operator.code", "Невірний код оператора");
        }

        public static class LastName
        {
            public static Error LastNameCantBeEmpty() => Error.Validation("last.name.cant.be.empty", "Прізвище є обов’язковим");
            public static Error LastNameInvalidMinLength() => Error.Validation("last.name.invalid.min.length", $"Прізвище має містити принаймні 2 символи");
            public static Error LastNameInvalidMaxLength() => Error.Validation("last.name.invalid.max.length", $"Прізвище не має перевищувати 50 символів");
            public static Error LastNameInvalidFormat() => Error.Validation("last.name.invalid.format", $"Допустимі лише літери, пробіл, дефіс та апостроф");
            public static Error LastNameInvalidLanguage() => Error.Validation("last.name.invalid.language", $"Прізвище має бути введене українською мовою");
        }

        public static class FirstName
        {
            public static Error FirstNameCantBeEmpty() => Error.Validation("first.name.cant.be.empty", "Ім'я є обов’язковим");
            public static Error FirstNameInvalidMinLength() => Error.Validation("first.name.invalid.min.length", $"Ім'я має містити принаймні 2 символи");
            public static Error FirstNameInvalidMaxLength() => Error.Validation("first.name.invalid.max.length", $"Ім'я не має перевищувати 50 символів");
            public static Error FirstNameInvalidFormat() => Error.Validation("first.name.invalid.format", $"Допустимі лише літери, пробіл, дефіс та апостроф");
            public static Error FirstNameInvalidLanguage() => Error.Validation("first.name.invalid.language", $"Ім'я має бути введене українською мовою");
        }

        public static class CategoryName
        {
            public static Error CategoryNameCantBeEmpty() => Error.Validation("category.name.cant.be.empty", "Назва категорії є обов'язковою");
            public static Error CategoryNameInvalidMinLength() => Error.Validation("category.name.invalid.min.length", $"Назва категорії має містити принаймні 2 символи");
            public static Error CategoryNameInvalidMaxLength() => Error.Validation("category.name.invalid.max.length", $"Назва категорії не має перевищувати 50 символів");
            public static Error CategoryNameInvalidFormat() => Error.Validation("category.name.invalid.format", $"Допустимі лише літери, пробіл, дефіс та апостроф");
            public static Error CategoryNameInvalidLanguage() => Error.Validation("category.name.invalid.language", $"Назва категорії має бути введене українською мовою");
        }

        public static class Patronymic
        {
            public static Error PatronymicCantBeEmpty() => Error.Validation("patronymic.cant.be.empty", "По-батькові є обов’язковим");
            public static Error PatronymicInvalidMinLength() => Error.Validation("patronymic.invalid.min.length", $"По-батькові має містити принаймні 2 символи");
            public static Error PatronymicInvalidMaxLength() => Error.Validation("patronymic.invalid.max.length", $"По-батькові не має перевищувати 50 символів");
            public static Error PatronymicInvalidFormat() => Error.Validation("patronymic.invalid.format", $"Допустимі лише літери, пробіл, дефіс та апостроф");
            public static Error PatronymicInvalidLanguage() => Error.Validation("patronymic.invalid.language", $"По-батькові має бути введене українською мовою");
        }

        public static class Authentication
        {
            public static Error RefreshTokenIsNull() => Error.Validation("refresh.token.is.null", "Помилка при обробці запиту");
            public static Error RefreshTokenIsInvalid() => Error.Unauthorized("refresh.token.is.invalid", "Помилка при обробці запиту");
            public static Error Unauthorized() => Error.Unauthorized("Unauthorized", "Неавторизовано");
            public static Error UserIsAlreadyExistsByEmail() => Error.Validation("user.is.already.exists.by.email", "Користувач з такою адресою вже існує");
            public static Error UserIsAlreadyExistsByPhone() => Error.Validation("user.is.already.exists.by.phone", "Користувач з таким номером вже існує");
            public static Error IncorrectPhone() => Error.Validation("incorrect.phone", "Невірний номер");
            public static Error IncorrectPassword() => Error.Validation("incorrect.password", "Невірний пароль");
        }
    }
}
