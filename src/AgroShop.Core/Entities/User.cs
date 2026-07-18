using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;

namespace AgroShop.Core.Entities
{
    public class User
    {
        private User() { }
        private readonly HashSet<RefreshToken> _refreshTokens = new();

        public Guid Id { get; private set; }
        public LastName LastName { get; private set; } = null!;
        public FirstName FirstName { get; private set; } = null!;
        public Patronymic Patronymic { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public Phone Phone { get; private set; } = null!;
        public string PasswordHash { get; private set; } = null!;
        public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
        public Guid RoleId { get; private set; }
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;
        public Role Role { get; private set; } = null!;

        public static Result<User, Error> Create(
            string lastName, string firstName, string patronymic,
            string email, string phone, string passwordHash, Guid roleId)
        {
            return LastName.Create(lastName).Bind(ln =>
                FirstName.Create(firstName).Bind(fn =>
                Patronymic.Create(patronymic).Bind(pt =>
                Email.Create(email).Bind(em =>
                Phone.Create(phone).Map(ph =>
                new User
                {
                    Id = Guid.CreateVersion7(),
                    LastName = ln,
                    FirstName = fn,
                    Patronymic = pt,
                    Email = em,
                    Phone = ph,
                    PasswordHash = passwordHash,
                    RoleId = roleId
                })))));
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
