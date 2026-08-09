using AgroShop.Application.Dto.AttributeDto;
using AgroShop.Application.Interfaces;
using AgroShop.Application.Mappers;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;
using Attribute = AgroShop.Core.Entities.Attribute;

namespace AgroShop.Application.Services
{
    public class AttributeService : IAttributeService
    {
        // Backstop only - Add/Update/Delete below invalidate this explicitly.
        private const string AttributesCacheKey = "attributes:all";
        private static readonly TimeSpan AttributesCacheDuration = TimeSpan.FromMinutes(15);

        private readonly IAttributeRepository _attributeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public AttributeService(
            IAttributeRepository attributeRepository,
            IUnitOfWork unitOfWork,
            ICacheService cacheService)
        {
            _attributeRepository = attributeRepository;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<Result<IEnumerable<AttributeDto>, Error>> GetAttributesAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var cached = await _cacheService.GetAsync<List<AttributeDto>>(AttributesCacheKey, cancellationToken);
            if (cached != null)
                return Result.Success<IEnumerable<AttributeDto>, Error>(cached);

            var attributes = await _attributeRepository.GetAttributesAsync(asNoTracking, cancellationToken);
            var attributeDtos = attributes.ToDto().OrderBy(a => a.Name).ToList();

            await _cacheService.SetAsync(AttributesCacheKey, attributeDtos, AttributesCacheDuration, cancellationToken);

            return Result.Success<IEnumerable<AttributeDto>, Error>(attributeDtos);
        }

        public async Task<Result<AttributeDto, Error>> GetAttributeByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var attributeId))
                return Result.Failure<AttributeDto, Error>(Errors.General.IncorrectGuidError());

            var attribute = await _attributeRepository.GetAttributeByIdAsync(attributeId, asNoTracking, cancellationToken);

            if (attribute == null)
                return Result.Failure<AttributeDto, Error>(Errors.Attribute.AttributeNotFoundById());

            return Result.Success<AttributeDto, Error>(attribute.ToDto());
        }

        public async Task<UnitResult<Error>> AddAsync(AddAttributeDto attributeDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var attributeResult = Attribute.Create(attributeDto.Name, attributeDto.ValueType);
            if (attributeResult.IsFailure)
                return UnitResult.Failure(attributeResult.Error);

            await _attributeRepository.AddAsync(attributeResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(AttributesCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }

        public async Task<Result<AttributeDto, Error>> UpdateAsync(string id, UpdateAttributeDto attributeDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var attributeId))
                return Result.Failure<AttributeDto, Error>(Errors.General.IncorrectGuidError());

            var attribute = await _attributeRepository.GetAttributeByIdAsync(attributeId, cancellationToken: cancellationToken);
            if (attribute == null)
                return Result.Failure<AttributeDto, Error>(Errors.Attribute.AttributeNotFoundById());

            var updateResult = attribute.Update(attributeDto.Name);
            if (updateResult.IsFailure)
                return Result.Failure<AttributeDto, Error>(updateResult.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(AttributesCacheKey, cancellationToken);

            return Result.Success<AttributeDto, Error>(attribute.ToDto());
        }

        public async Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var attributeId))
                return UnitResult.Failure(Errors.General.IncorrectGuidError());

            var attribute = await _attributeRepository.GetAttributeByIdAsync(attributeId, cancellationToken: cancellationToken);
            if (attribute == null)
                return UnitResult.Failure(Errors.Attribute.AttributeNotFoundById());

            _attributeRepository.Delete(attribute);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(AttributesCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }
    }
}
