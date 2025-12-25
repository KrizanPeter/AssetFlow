using AssetFlow.Application.Dtos.Asset;
using FluentResults;
using MediatR;

namespace AssetFlow.Application.MediatR.Queries
{
    public class GetAssetsQuery : IRequest<Result<AssetsDto>>
    {
        public Guid AccountId { get; set; }

        private GetAssetsQuery(Guid accountId)
        {
            this.AccountId = accountId;
        }


        public static GetAssetsQuery Of(Guid accountId)
        {
            return new GetAssetsQuery(accountId);
        }
    }
}
