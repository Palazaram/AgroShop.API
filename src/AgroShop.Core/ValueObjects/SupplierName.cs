using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace AgroShop.Core.ValueObjects
{
    public class SupplierName : ValueObject
    {
        public string Value { get; }

        protected static int minLength = 2;
        protected static int maxLength = 50;

        private SupplierName(string value)
        {
            Value = value;
        }

        public static Result<SupplierName, Error> Create(string supplierName)
        {
            if (string.IsNullOrWhiteSpace(supplierName))
                return Result.Failure<SupplierName, Error>(Errors.SupplierName.SupplierNameCantBeEmpty());

            supplierName = supplierName.Trim();

            if (supplierName.Length < minLength)
                return Result.Failure<SupplierName, Error>(Errors.SupplierName.SupplierNameInvalidMinLength(minLength));

            if (supplierName.Length > maxLength)
                return Result.Failure<SupplierName, Error>(Errors.SupplierName.SupplierNameInvalidMaxLength(maxLength));

            return Result.Success<SupplierName, Error>(new SupplierName(supplierName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
