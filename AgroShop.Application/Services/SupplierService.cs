using AgroShop.Application.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using AgroShop.Core.ValueObjects;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }

        public async Task<Result<IEnumerable<Supplier>, Error>> GetSuppliersAsync(CancellationToken cancellationToken, bool asNoTracking = false, Func<IQueryable<Supplier>, IQueryable<Supplier>>? filter = null)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var suppliers = await _supplierRepository.GetSuppliersAsync(cancellationToken, asNoTracking, filter);
            return Result.Success<IEnumerable<Supplier>, Error>(suppliers);
        }

        public async Task<Result<Supplier?, Error>> GetSupplierByIdAsync(Guid id, CancellationToken cancellationToken, bool asNoTracking = false)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id, cancellationToken, asNoTracking);
            return Result.Success<Supplier?, Error>(supplier);
        }
    }
}
