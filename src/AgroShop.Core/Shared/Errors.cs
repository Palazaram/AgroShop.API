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

        public static class SubCategory
        {
            public static Error SubCategoryIsNullById() => Error.Validation("sub.category.is.null.by.id", "Підкатегорію з даним ID не знайдено");
            public static Error SubCategoryNameAlreadyExistsInCategory() => Error.Validation("sub.category.name.already.exists.in.category", "Підкатегорія з такою назвою вже існує в цій категорії");
        }

        public static class SubCategoryName
        {
            public static Error SubCategoryNameCantBeEmpty() => Error.Validation("sub.category.name.cant.be.empty", "Назва підкатегорії є обов'язковою");
            public static Error SubCategoryNameInvalidMinLength(int minLength) => Error.Validation("sub.category.name.invalid.min.length", $"Назва підкатегорії має містити принаймні {minLength} символи");
            public static Error SubCategoryNameInvalidMaxLength(int maxLength) => Error.Validation("sub.category.name.invalid.max.length", $"Назва підкатегорії не має перевищувати {maxLength} символів");
            public static Error SubCategoryNameInvalidFormat() => Error.Validation("sub.category.name.invalid.format", $"Допустимі лише літери, пробіл, дефіс та апостроф");
            public static Error SubCategoryNameInvalidLanguage() => Error.Validation("sub.category.name.invalid.language", $"Назва підкатегорії має бути введене українською мовою");
        }

        public static class ProductName
        {
            public static Error ProductNameCantBeEmpty() => Error.Validation("product.name.cant.be.empty", "Назва продукту є обов'язковою");
            public static Error ProductNameInvalidMinLength(int minLength) => Error.Validation("product.name.invalid.min.length", $"Назва продукту має містити принаймні {minLength} символи");
            public static Error ProductNameInvalidMaxLength(int maxLength) => Error.Validation("product.name.invalid.max.length", $"Назва продукту не має перевищувати {maxLength} символів");
            public static Error ProductNameInvalidFormat() => Error.Validation("product.name.invalid.format", $"Допустимі лише літери, пробіл, дефіс та апостроф");
            public static Error ProductNameInvalidLanguage() => Error.Validation("product.name.invalid.language", $"Назва продукту має бути введене українською мовою");
        }

        public static class Product
        {
            public static Error ProductIsNullById() => Error.Validation("product.is.null.by.id", "Товар з даним ID не знайдено");
            public static Error SkuAlreadyExists() => Error.Validation("product.sku.already.exists", "Товар з таким SKU вже існує");
        }

        public static class ProductDescription
        {
            public static Error ProductDescriptionCantBeEmpty() => Error.Validation("product.description.cant.be.empty", "Опис товару є обов'язковим");
            public static Error ProductDescriptionInvalidMinLength(int minLength) => Error.Validation("product.description.invalid.min.length", $"Опис товару має містити принаймні {minLength} символів");
            public static Error ProductDescriptionInvalidMaxLength(int maxLength) => Error.Validation("product.description.invalid.max.length", $"Опис товару не має перевищувати {maxLength} символів");
        }

        public static class Sku
        {
            public static Error SkuCantBeEmpty() => Error.Validation("sku.cant.be.empty", "SKU є обов'язковим");
            public static Error SkuInvalidMinLength(int minLength) => Error.Validation("sku.invalid.min.length", $"SKU має містити принаймні {minLength} символи");
            public static Error SkuInvalidMaxLength(int maxLength) => Error.Validation("sku.invalid.max.length", $"SKU не має перевищувати {maxLength} символів");
            public static Error SkuInvalidFormat() => Error.Validation("sku.invalid.format", "SKU може містити лише латинські літери, цифри, дефіс та підкреслення");
        }

        public static class StockQuantity
        {
            public static Error StockQuantityCantBeNegative() => Error.Validation("stock.quantity.cant.be.negative", "Кількість на складі не може бути від'ємною");
        }

        public static class Money
        {
            public static Error MoneyMustBePositive() => Error.Validation("money.must.be.positive", "Сума повинна бути більшою за нуль");
            public static Error MoneyInvalidPrecision() => Error.Validation("money.invalid.precision", "Сума не може містити більше 2 знаків після коми");
        }

        public static class Supplier
        {
            public static Error SupplierIsNullById() => Error.Validation("supplier.is.null.by.id", "Постачальника з даним ID не знайдено");
        }

        public static class SupplierName
        {
            public static Error SupplierNameCantBeEmpty() => Error.Validation("supplier.name.cant.be.empty", "Назва постачальника є обов'язковою");
            public static Error SupplierNameInvalidMinLength(int minLength) => Error.Validation("supplier.name.invalid.min.length", $"Назва постачальника має містити принаймні {minLength} символи");
            public static Error SupplierNameInvalidMaxLength(int maxLength) => Error.Validation("supplier.name.invalid.max.length", $"Назва постачальника не має перевищувати {maxLength} символів");
        }

        public static class Image
        {
            public static Error InvalidFormat() => Error.Validation("image.invalid.format", "Дозволені лише зображення формату JPG, PNG або WEBP");
            public static Error InvalidSize(int maxMegabytes) => Error.Validation("image.invalid.size", $"Розмір зображення не має перевищувати {maxMegabytes} МБ");
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
            public static Error LastNameInvalidMinLength(int minLength) => Error.Validation("last.name.invalid.min.length", $"Прізвище має містити принаймні {minLength} символи");
            public static Error LastNameInvalidMaxLength(int maxLength) => Error.Validation("last.name.invalid.max.length", $"Прізвище не має перевищувати {maxLength} символів");
            public static Error LastNameInvalidFormat() => Error.Validation("last.name.invalid.format", $"Допустимі лише літери, пробіл, дефіс та апостроф");
            public static Error LastNameInvalidLanguage() => Error.Validation("last.name.invalid.language", $"Прізвище має бути введене українською мовою");
        }

        public static class FirstName
        {
            public static Error FirstNameCantBeEmpty() => Error.Validation("first.name.cant.be.empty", "Ім'я є обов’язковим");
            public static Error FirstNameInvalidMinLength(int minLength) => Error.Validation("first.name.invalid.min.length", $"Ім'я має містити принаймні {minLength} символи");
            public static Error FirstNameInvalidMaxLength(int maxLength) => Error.Validation("first.name.invalid.max.length", $"Ім'я не має перевищувати {maxLength} символів");
            public static Error FirstNameInvalidFormat() => Error.Validation("first.name.invalid.format", $"Допустимі лише літери, пробіл, дефіс та апостроф");
            public static Error FirstNameInvalidLanguage() => Error.Validation("first.name.invalid.language", $"Ім'я має бути введене українською мовою");
        }

        public static class CategoryName
        {
            public static Error CategoryNameCantBeEmpty() => Error.Validation("category.name.cant.be.empty", "Назва категорії є обов'язковою");
            public static Error CategoryNameInvalidMinLength(int minLength) => Error.Validation("category.name.invalid.min.length", $"Назва категорії має містити принаймні {minLength} символи");
            public static Error CategoryNameInvalidMaxLength(int maxLength) => Error.Validation("category.name.invalid.max.length", $"Назва категорії не має перевищувати {maxLength} символів");
            public static Error CategoryNameInvalidFormat() => Error.Validation("category.name.invalid.format", $"Допустимі лише літери, пробіл, дефіс та апостроф");
            public static Error CategoryNameInvalidLanguage() => Error.Validation("category.name.invalid.language", $"Назва категорії має бути введене українською мовою");
        }

        public static class Patronymic
        {
            public static Error PatronymicCantBeEmpty() => Error.Validation("patronymic.cant.be.empty", "По-батькові є обов’язковим");
            public static Error PatronymicInvalidMinLength(int minLength) => Error.Validation("patronymic.invalid.min.length", $"По-батькові має містити принаймні {minLength} символи");
            public static Error PatronymicInvalidMaxLength(int maxLength) => Error.Validation("patronymic.invalid.max.length", $"По-батькові не має перевищувати {maxLength} символів");
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
