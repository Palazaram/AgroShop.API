using AgroShop.Core.Entities;

namespace AgroShop.Application.Responses
{
    public class UserResponse
    {
        public Guid Id { get; init; }
        public string Phone { get; init; } = default!;
        public string LastName { get; init; } = default!;
        public string FirstName { get; init; } = default!;
        public string Patronymic { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Role { get; init; } = default!;

        public static UserResponse FromEntity(User user) => new()
        {
            Id = user.Id,
            Phone = user.Phone.Value,
            LastName = user.LastName.Value,
            FirstName = user.FirstName.Value,
            Patronymic = user.Patronymic.Value,
            Email = user.Email.Value,
            Role = user.Role.Name
        };
    }
}
