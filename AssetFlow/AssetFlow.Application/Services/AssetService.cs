using AssetFlow.Application.Dtos.Asset;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities.EventAggregates;
using AssetFlow.Domain.Events;
using FluentResults;
using MapsterMapper;
using Microsoft.Extensions.Logging;


namespace AssetFlow.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AssetService> _logger;
        private readonly IMapper _mapper;
        public AssetService(IUnitOfWork unitOfWork, ILogger<AssetService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Result<Guid>> CreateAsset(SnapshotAssetCreated asset)
        {
            try
            {
                await _unitOfWork.Events.StartStreamAsync(asset.AssetId, asset);
                await _unitOfWork.CommitAsync();

                return Result.Ok(asset.AssetId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating asset with ID {AssetId}", asset.AssetId);
                return Result.Fail("An error occurred while creating the asset.");
            }
        }

        public async Task<Result<Guid>> CreateAsset(LedgerAssetCreated asset)
        {
            try
            {
                var streamId = Guid.NewGuid();
                await _unitOfWork.Events.StartStreamAsync(streamId, asset);
                await _unitOfWork.CommitAsync();

                return Result.Ok(streamId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating asset with ID {AssetId}", asset.AssetId);
                return Result.Fail("An error occurred while creating the asset.");
            }
        }

        public async Task<Result<AssetDto>> GetAssetById(Guid assetId)
        {
            try
            {
                var result = await _unitOfWork.Events.AggregateStreamAsync<SnapshotAsset>(assetId);

                if (result is null) 
                    throw new KeyNotFoundException($"Asset with {assetId} not found");

                var dto = _mapper.Map<AssetDto>(result);

                return Result.Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting asset with ID {AssetId}", assetId);
                return Result.Fail("An error occurred while getting the asset.");
            }
        }

        public Task<Result<AssetsDto>> GetAssetsByAccountId(Guid accountId)
        {
            throw new NotImplementedException();
        }
    }
}
