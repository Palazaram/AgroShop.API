using AgroShop.Core.ValueObjects;

namespace AgroShop.Core.Entities
{
    public class User
    {
        private User() { }
        private readonly HashSet<RefreshToken> _refreshTokens = new();

        public Guid Id { get; private set; }
        public LastName LastName { get; private set; } = null!;
        public FirstName FirstName { get; private set; } = null!;
        public Patronymic? Patronymic { get; private set; }
        public Email Email { get; private set; } = null!;
        public Phone Phone { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
        public Guid RoleId { get; private set; }
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
        public Role Role { get; private set; } = null!;

        public static User Create(
            LastName lastName, FirstName firstName, Patronymic? patronymic,
            Email email, Phone phone, string passwordHash, Guid roleId)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                LastName = lastName,
                FirstName = firstName,
                Patronymic = patronymic,
                Email = email,
                Phone = phone,
                PasswordHash = passwordHash,
                RoleId = roleId
            };

            return user;
        }

        public void AssignRole(Role role)
        {
            Role = role;
        }

        public void AddRefreshToken(RefreshToken refreshToken)
        {
            _refreshTokens.Add(refreshToken);
        }
    }
}
