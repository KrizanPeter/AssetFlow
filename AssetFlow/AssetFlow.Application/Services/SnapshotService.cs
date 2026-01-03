using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities.EventAggregates;
using AssetFlow.Domain.Events;
using FluentResults;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace AssetFlow.Application.Services
{
    public class SnapshotService : ISnapshotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SnapshotService> _logger;
        private readonly IMapper _mapper;
        public SnapshotService(IUnitOfWork unitOfWork, ILogger<SnapshotService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Result<Guid>> CreateSnapshot(Guid assetId, SnapshotCreated snapshot)
        {
            try
            {
                await _unitOfWork.Events.AppendEventAsync(assetId, snapshot);
                await _unitOfWork.CommitAsync();

                return Result.Ok(snapshot.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating snapshot for asset with ID {AssetId}", assetId);
                return Result.Fail($"Error creating snapshot for asset with ID {assetId}.");
            }
        }
    }
}
