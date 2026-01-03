
using AssetFlow.Domain.Events;
using FluentResults;

namespace AssetFlow.Application.Interfaces.IServices
{
    public interface ISnapshotService
    {
        Task<Result<Guid>> CreateSnapshot(Guid assetId, SnapshotCreated snapshot);
    }
}
