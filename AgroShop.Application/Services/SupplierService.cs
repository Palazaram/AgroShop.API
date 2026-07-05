using AgroShop.Application.Interfaces;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
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

        public async Task<Result<IEnumerable<Supplier>, Error>> GetSuppliersAsync(bool asNoTracking = false, Func<IQueryable<Supplier>, IQueryable<Supplier>>? filter = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var suppliers = await _supplierRepository.GetSuppliersAsync(asNoTracking, filter, cancellationToken);
            return Result.Success<IEnumerable<Supplier>, Error>(suppliers);
        }

        public async Task<Result<Supplier?, Error>> GetSupplierByIdAsync(Guid id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var supplier = await _supplierRepository.GetSupplierByIdAsync(id, asNoTracking, cancellationToken);
            return Result.Success<Supplier?, Error>(supplier);
        }
    }
}
