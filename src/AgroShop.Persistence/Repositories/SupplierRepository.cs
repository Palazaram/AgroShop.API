using AgroShop.Core.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AgroShop.Persistence.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AgroShopDbContext _context;

        public SupplierRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var suppliersQuery = _context.Suppliers.IncludeAll();

            if (asNoTracking)
            {
                suppliersQuery = suppliersQuery.AsNoTracking();
            }

            return await suppliersQuery.ToListAsync(cancellationToken);
        }

        public async Task<Supplier?> GetSupplierByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            var suppliersQuery = _context.Suppliers.IncludeAll();

            if (asNoTracking)
            {
                suppliersQuery = suppliersQuery.AsNoTracking();
            }

            return await suppliersQuery.SingleOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task AddAsync(Supplier supplier, CancellationToken cancellationToken)
        {
            await _context.Suppliers.AddAsync(supplier, cancellationToken);
        }

        public void Delete(Supplier supplier)
        {
            _context.Entry(supplier).State = EntityState.Deleted;
        }
    }
}
