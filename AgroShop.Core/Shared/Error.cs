using AgroShop.Core.Enums;

namespace AgroShop.Core.Shared
{
    public sealed record Error
    {
        private const string Separator = "||";

        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }

        private Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }

        public static Error Validation(string code, string message) => new Error(code, message, ErrorType.Validation);

        public static Error NotFound(string code, string message) => new Error(code, message, ErrorType.NotFound);

        public static Error Conflict(string code, string message) => new Error(code, message, ErrorType.Conflict);

        public static Error Unauthorized(string code, string message) => new Error(code, message, ErrorType.Unauthorized);

        public static Error InternalServerError(string code, string message) => new Error(code, message, ErrorType.InternalServerError);

        public string Serialize() => string.Join(Separator, Code, Message, Type);

        public static Error Deserialize(string serialized) 
        {
            var parts = serialized.Split(Separator, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length > 3) 
            {
                throw new ArgumentException($"Invalid error serialization: '{serialized}'");
            }

            if (Enum.TryParse<ErrorType>(parts[2], out var type) == false) 
            {
                throw new ArgumentException($"Invalid error serialization: '{serialized}'");
            }

            return new Error(parts[0], parts[1], type);
        }
    }
}
