using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace AgroShop.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly AgroShopDbContext _context;

        public RoleRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetRoleByIdAsync(CancellationToken cancellationToken, Guid Id)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await _context.Roles.SingleOrDefaultAsync(r => r.Id == Id, cancellationToken);
        }
    }
}
