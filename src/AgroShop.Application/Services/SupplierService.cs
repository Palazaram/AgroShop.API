using AgroShop.Application.Dto.SupplierDto;
using AgroShop.Application.Interfaces;
using AgroShop.Application.Mappers;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Services
{
    public class SupplierService : ISupplierService
    {
        // Backstop only - Add/Update/Delete below invalidate this explicitly.
        private const string SuppliersCacheKey = "suppliers:all";
        private static readonly TimeSpan SuppliersCacheDuration = TimeSpan.FromMinutes(15);

        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public SupplierService(
            ISupplierRepository supplierRepository,
            IUnitOfWork unitOfWork,
            ICacheService cacheService)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<Result<IEnumerable<SupplierDto>, Error>> GetSuppliersAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var cached = await _cacheService.GetAsync<List<SupplierDto>>(SuppliersCacheKey, cancellationToken);
            if (cached != null)
                return Result.Success<IEnumerable<SupplierDto>, Error>(cached);

            var suppliers = await _supplierRepository.GetSuppliersAsync(asNoTracking, cancellationToken);
            var supplierDtos = suppliers.ToDto().OrderBy(s => s.Name).ToList();

            await _cacheService.SetAsync(SuppliersCacheKey, supplierDtos, SuppliersCacheDuration, cancellationToken);

            return Result.Success<IEnumerable<SupplierDto>, Error>(supplierDtos);
        }

        public async Task<Result<SupplierDto, Error>> GetSupplierByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var supplierId))
                return Result.Failure<SupplierDto, Error>(Errors.General.IncorrectGuidError());

            var supplier = await _supplierRepository.GetSupplierByIdAsync(supplierId, asNoTracking, cancellationToken);

            if (supplier == null)
                return Result.Failure<SupplierDto, Error>(Errors.Supplier.SupplierIsNullById());

            return Result.Success<SupplierDto, Error>(supplier.ToDto());
        }

        public async Task<UnitResult<Error>> AddAsync(AddSupplierDto supplierDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var supplierResult = Supplier.Create(supplierDto.Name);
            if (supplierResult.IsFailure)
                return UnitResult.Failure(supplierResult.Error);

            await _supplierRepository.AddAsync(supplierResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(SuppliersCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }

        public async Task<Result<SupplierDto, Error>> UpdateAsync(string id, UpdateSupplierDto supplierDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var supplierId))
                return Result.Failure<SupplierDto, Error>(Errors.General.IncorrectGuidError());

            var supplier = await _supplierRepository.GetSupplierByIdAsync(supplierId, cancellationToken: cancellationToken);
            if (supplier == null)
                return Result.Failure<SupplierDto, Error>(Errors.Supplier.SupplierIsNullById());

            var updateResult = supplier.Update(supplierDto.Name);
            if (updateResult.IsFailure)
                return Result.Failure<SupplierDto, Error>(updateResult.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(SuppliersCacheKey, cancellationToken);

            return Result.Success<SupplierDto, Error>(supplier.ToDto());
        }

        public async Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var supplierId))
                return UnitResult.Failure(Errors.General.IncorrectGuidError());

            var supplier = await _supplierRepository.GetSupplierByIdAsync(supplierId, cancellationToken: cancellationToken);
            if (supplier == null)
                return UnitResult.Failure(Errors.Supplier.SupplierIsNullById());

            _supplierRepository.Delete(supplier);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(SuppliersCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }
    }
}
