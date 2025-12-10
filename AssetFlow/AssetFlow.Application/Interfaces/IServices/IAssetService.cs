
using AssetFlow.Domain.Entities.EventAggregates;
using FluentResults;

namespace AssetFlow.Application.Interfaces.IServices
{
    public interface IAssetService
    {
        Task<Result> CreateAsset(Asset asset);
    }
}
