using AgroShop.Core.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Core.ValueObjects;
using AgroShop.Persistence.Data;
using AgroShop.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AgroShop.Persistence.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AgroShopDbContext _context;

        public SupplierRepository(AgroShopDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Supplier>> GetSuppliersAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<Supplier>, IQueryable<Supplier>>? filter = null)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var suppliersQuery = _context.Suppliers.IncludeAll();

            if (asNoTracking)
            {
                suppliersQuery = suppliersQuery.AsNoTracking();
            }

            if (filter != null)
            {
                suppliersQuery = filter(suppliersQuery);
            }

            return await suppliersQuery.ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<TDto>> GetSuppliersDTOAsync<TDto>(Expression<Func<Supplier, TDto>> selector, CancellationToken cancellationToken, Func<IQueryable<Supplier>, IQueryable<Supplier>>? filter = null)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var suppliersQuery = _context.Suppliers.AsNoTracking();

            if (filter != null)
            {
                suppliersQuery = filter(suppliersQuery);
            }

            return await suppliersQuery.Select(selector).ToListAsync(cancellationToken);
        }

        public async Task<Supplier?> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var suppliersQuery = _context.Suppliers.IncludeAll();

            if (asNoTracking)
            {
                suppliersQuery = suppliersQuery.AsNoTracking();
            }

            return await suppliersQuery.FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task AddSupplierAsync(Supplier supplier, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _context.Suppliers.AddAsync(supplier);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateSupplierAsync(Supplier supplier, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteSupplierAsync(Supplier supplier, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            _context.Entry(supplier).State = EntityState.Deleted;
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
