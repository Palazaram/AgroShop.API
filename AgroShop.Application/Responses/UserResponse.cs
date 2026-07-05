namespace AgroShop.Application.Responses
{
    public class UserResponse
    {
        public Guid Id { get; init; }
        public string Phone { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string FirstName { get; init; } = default!;
        public string? Patronymic { get; init; }
        public string? Email { get; init; }
        public string Role { get; init; } = default!;
    }
}
