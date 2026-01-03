using AssetFlow.Application.Dtos.Asset;
using FluentResults;
using MediatR;

namespace AssetFlow.Application.MediatR.Commands
{
    public class AddSnapshotCommand : IRequest<Result<Guid>>
    {
        public Guid AssetId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Balance { get; set; } = decimal.Zero;

        public static AddSnapshotCommand Of(AddSnapshotDto dto)
        {
            return new AddSnapshotCommand
            {
                AssetId = dto.AssetId,
                CreatedAt = dto.CreatedAt,
                Balance = dto.Balance
            };
        }
    }
}
