namespace AgroShop.Core.Entities
{
    public class Role
    {
        private Role() { }
        public Guid Id { get; private set; }
        public string Name { get; private set; } = null!;
    }
}
