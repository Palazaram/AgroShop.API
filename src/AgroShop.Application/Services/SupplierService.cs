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
        private const string ImagesSubfolder = "suppliers";

        // Backstop only - Add/Update/Delete below invalidate this explicitly.
        private const string SuppliersCacheKey = "suppliers:all";
        private static readonly TimeSpan SuppliersCacheDuration = TimeSpan.FromMinutes(15);

        private readonly ISupplierRepository _supplierRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IImageStorageService _imageStorageService;
        private readonly ICacheService _cacheService;

        public SupplierService(
            ISupplierRepository supplierRepository,
            IUnitOfWork unitOfWork,
            IImageStorageService imageStorageService,
            ICacheService cacheService)
        {
            _supplierRepository = supplierRepository;
            _unitOfWork = unitOfWork;
            _imageStorageService = imageStorageService;
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
                return Result.Failure<SupplierDto, Error>(Errors.Supplier.SupplierNotFoundById());

            return Result.Success<SupplierDto, Error>(supplier.ToDto());
        }

        public async Task<UnitResult<Error>> AddAsync(AddSupplierDto supplierDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            string? imagePath = null;
            if (supplierDto.Image != null)
                imagePath = await _imageStorageService.SaveAsync(supplierDto.Image, ImagesSubfolder, cancellationToken);

            var supplierResult = Supplier.Create(supplierDto.Name, imagePath);
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
                return Result.Failure<SupplierDto, Error>(Errors.Supplier.SupplierNotFoundById());

            var previousImagePath = supplier.ImagePath;

            string? imagePath = null;
            if (supplierDto.Image != null)
                imagePath = await _imageStorageService.SaveAsync(supplierDto.Image, ImagesSubfolder, cancellationToken);

            var updateResult = supplier.Update(supplierDto.Name, imagePath);
            if (updateResult.IsFailure)
                return Result.Failure<SupplierDto, Error>(updateResult.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(SuppliersCacheKey, cancellationToken);

            // Only drop the old file once the new one is safely persisted.
            if (imagePath != null)
                await _imageStorageService.DeleteAsync(previousImagePath, cancellationToken);

            return Result.Success<SupplierDto, Error>(supplier.ToDto());
        }

        public async Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var supplierId))
                return UnitResult.Failure(Errors.General.IncorrectGuidError());

            var supplier = await _supplierRepository.GetSupplierByIdAsync(supplierId, cancellationToken: cancellationToken);
            if (supplier == null)
                return UnitResult.Failure(Errors.Supplier.SupplierNotFoundById());

            _supplierRepository.Delete(supplier);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(SuppliersCacheKey, cancellationToken);
            await _imageStorageService.DeleteAsync(supplier.ImagePath, cancellationToken);
            return UnitResult.Success<Error>();
        }
    }
}
