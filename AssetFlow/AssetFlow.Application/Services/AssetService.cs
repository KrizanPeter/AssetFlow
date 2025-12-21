using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Events;
using FluentResults;
using Microsoft.Extensions.Logging;


namespace AssetFlow.Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AssetService> _logger;
        public AssetService(IUnitOfWork unitOfWork, ILogger<AssetService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<Guid>> CreateAsset(SnapshotAssetCreated asset)
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
    }
}
