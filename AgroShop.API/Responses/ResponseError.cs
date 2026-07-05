namespace AgroShop.API.Responses
{
    public record ResponseError(string? ErrorCode, string? ErrorMessage, string? InvalidField);
}
