namespace AgroShop.Core.Entities
{
    public class RefreshToken
    {
        private RefreshToken() { }

        public Guid Id { get; private set; }

        public Guid UserId { get; private set; }
        public virtual User User { get; set; } = default!;

        public string TokenHash { get; set; } = null!;

        public bool IsRevoked { get; set; } = false;

        public DateTime ExpiryDate { get; private set; }
        public DateTime? RevokeDate { get; private set; }

        public static RefreshToken Create(Guid userId, string tokenHash, DateTime expiryDate)
        {
            return new RefreshToken
            {
                UserId = userId,
                TokenHash = tokenHash,
                ExpiryDate = expiryDate
            };
        }

        public void Revoke()
        {
            if (!IsRevoked)
            {
                IsRevoked = true;
                RevokeDate = DateTime.UtcNow;
            }
        }
    }
}
