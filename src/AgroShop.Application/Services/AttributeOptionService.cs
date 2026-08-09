using AgroShop.Application.Dto.AttributeOptionDto;
using AgroShop.Application.Interfaces;
using AgroShop.Application.Mappers;
using AgroShop.Core.Entities;
using AgroShop.Core.Interfaces;
using AgroShop.Core.Shared;
using CSharpFunctionalExtensions;

namespace AgroShop.Application.Services
{
    public class AttributeOptionService : IAttributeOptionService
    {
        // Backstop only - Add/Update/Delete below invalidate this explicitly.
        private const string AttributeOptionsCacheKey = "attribute-options:all";
        private static readonly TimeSpan AttributeOptionsCacheDuration = TimeSpan.FromMinutes(15);

        private readonly IAttributeOptionRepository _attributeOptionRepository;
        private readonly IAttributeRepository _attributeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public AttributeOptionService(
            IAttributeOptionRepository attributeOptionRepository,
            IAttributeRepository attributeRepository,
            IUnitOfWork unitOfWork,
            ICacheService cacheService)
        {
            _attributeOptionRepository = attributeOptionRepository;
            _attributeRepository = attributeRepository;
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        public async Task<Result<IEnumerable<AttributeOptionDto>, Error>> GetAttributeOptionsAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var cached = await _cacheService.GetAsync<List<AttributeOptionDto>>(AttributeOptionsCacheKey, cancellationToken);
            if (cached != null)
                return Result.Success<IEnumerable<AttributeOptionDto>, Error>(cached);

            var options = await _attributeOptionRepository.GetAttributeOptionsAsync(asNoTracking, cancellationToken);
            var optionDtos = options.ToDto().OrderBy(o => o.AttributeName).ThenBy(o => o.Value).ToList();

            await _cacheService.SetAsync(AttributeOptionsCacheKey, optionDtos, AttributeOptionsCacheDuration, cancellationToken);

            return Result.Success<IEnumerable<AttributeOptionDto>, Error>(optionDtos);
        }

        public async Task<Result<AttributeOptionDto, Error>> GetAttributeOptionByIdAsync(string id, bool asNoTracking = false, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var optionId))
                return Result.Failure<AttributeOptionDto, Error>(Errors.General.IncorrectGuidError());

            var option = await _attributeOptionRepository.GetAttributeOptionByIdAsync(optionId, asNoTracking, cancellationToken);

            if (option == null)
                return Result.Failure<AttributeOptionDto, Error>(Errors.AttributeOption.AttributeOptionNotFoundById());

            return Result.Success<AttributeOptionDto, Error>(option.ToDto());
        }

        public async Task<UnitResult<Error>> AddAsync(AddAttributeOptionDto attributeOptionDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var attribute = await _attributeRepository.GetAttributeByIdAsync(attributeOptionDto.AttributeId, cancellationToken: cancellationToken);
            if (attribute == null)
                return UnitResult.Failure(Errors.Attribute.AttributeReferenceNotFound());

            if (await ValueTakenForAttributeAsync(attributeOptionDto.Value, attributeOptionDto.AttributeId, excludeId: null, cancellationToken))
                return UnitResult.Failure(Errors.AttributeOption.AttributeOptionAlreadyExistsForAttribute());

            var optionResult = AttributeOption.Create(attributeOptionDto.Value, attributeOptionDto.AttributeId);
            if (optionResult.IsFailure)
                return UnitResult.Failure(optionResult.Error);

            await _attributeOptionRepository.AddAsync(optionResult.Value, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(AttributeOptionsCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }

        public async Task<Result<AttributeOptionDto, Error>> UpdateAsync(string id, UpdateAttributeOptionDto attributeOptionDto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var optionId))
                return Result.Failure<AttributeOptionDto, Error>(Errors.General.IncorrectGuidError());

            var option = await _attributeOptionRepository.GetAttributeOptionByIdAsync(optionId, cancellationToken: cancellationToken);
            if (option == null)
                return Result.Failure<AttributeOptionDto, Error>(Errors.AttributeOption.AttributeOptionNotFoundById());

            if (await ValueTakenForAttributeAsync(attributeOptionDto.Value, option.AttributeId, excludeId: optionId, cancellationToken))
                return Result.Failure<AttributeOptionDto, Error>(Errors.AttributeOption.AttributeOptionAlreadyExistsForAttribute());

            var updateResult = option.Update(attributeOptionDto.Value);
            if (updateResult.IsFailure)
                return Result.Failure<AttributeOptionDto, Error>(updateResult.Error);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(AttributeOptionsCacheKey, cancellationToken);

            return Result.Success<AttributeOptionDto, Error>(option.ToDto());
        }

        public async Task<UnitResult<Error>> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (!Guid.TryParse(id, out var optionId))
                return UnitResult.Failure(Errors.General.IncorrectGuidError());

            var option = await _attributeOptionRepository.GetAttributeOptionByIdAsync(optionId, cancellationToken: cancellationToken);
            if (option == null)
                return UnitResult.Failure(Errors.AttributeOption.AttributeOptionNotFoundById());

            _attributeOptionRepository.Delete(option);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _cacheService.RemoveAsync(AttributeOptionsCacheKey, cancellationToken);
            return UnitResult.Success<Error>();
        }

        // Per-attribute uniqueness (not global - "Так" can be a valid option
        // under more than one attribute) enforced here, same reason as
        // SubCategoryService.NameTakenInCategoryAsync.
        private async Task<bool> ValueTakenForAttributeAsync(string value, Guid attributeId, Guid? excludeId, CancellationToken cancellationToken)
        {
            var options = await _attributeOptionRepository.GetAttributeOptionsAsync(asNoTracking: true, cancellationToken);
            var trimmedValue = value.Trim();

            return options.Any(o =>
                o.Id != excludeId
                && o.AttributeId == attributeId
                && string.Equals(o.Value.Value, trimmedValue, StringComparison.OrdinalIgnoreCase));
        }
    }
}
