
using AssetFlow.Application.Services;
using AssetFlow.Domain.Entities.EventAggregates;
using AssetFlow.Domain.Events;
using FluentResults;

namespace AssetFlow.Application.Interfaces.IServices
{
    public interface IAssetService
    {
        Task<Result<Guid>> CreateAsset(SnapshotAssetCreated asset);
        Task<Result<Guid>> CreateAsset(LedgerAssetCreated asset);

    }
}
