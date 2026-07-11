using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.ValueObjects
{
    public class Phone : ValueObject
    {
        public string Value { get; }

        protected static readonly int length = 9;
        protected static readonly string[] operatorCodes = {"50", "66", "75", "95", "99", "39", "67", "68", "77", "96", "97", "98", "63", "73", "93", "91", "92", "94"};

        private Phone(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static Result<Phone, Error> Create(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return Result.Failure<Phone, Error>(Errors.Phone.PhoneCantBeEmpty());

            if (phone.Length != length)
                return Result.Failure<Phone, Error>(Errors.Phone.PhoneInvalidLength());
  
            var code = phone.Substring(0, 2);
            if (!operatorCodes.Contains(code))
                return Result.Failure<Phone, Error>(Errors.Phone.PhoneInvalidOperatorCode());

            return Result.Success<Phone, Error>(new Phone(phone));
        }
    }
}
