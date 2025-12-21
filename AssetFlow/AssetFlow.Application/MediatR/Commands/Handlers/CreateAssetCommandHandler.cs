using AssetFlow.Application.Dtos.Asset;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Events;
using AssetFlow.Shared.Contexts;
using FluentResults;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssetFlow.Application.MediatR.Commands.Handlers
{
    internal class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Result<Guid>>
    {

        private readonly ILogger<CreateAssetCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IAssetService _assetService;
        private readonly IUserContext _userContext;

        public CreateAssetCommandHandler(ILogger<CreateAssetCommandHandler> logger,
            IMapper mapper,
            IAssetService assetService,
            IUserContext userContext)
        {
            _logger = logger;
            _mapper = mapper;
            _assetService = assetService;
            _userContext = userContext;
        }

        public async Task<Result<Guid>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var accountId = _userContext.AccountId;
                return request.TypeOfAsset switch
                {
                    AssetType.Snapshot => await HandleSnapshot(request, accountId),
                    AssetType.Ledger => await HandleLedger(request, accountId),
                    _ => throw new ArgumentOutOfRangeException(nameof(request.TypeOfAsset))
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }

        private async Task<Result<Guid>> HandleSnapshot(CreateAssetCommand request, Guid accountId)
        {
            var evt = _mapper.Map<SnapshotAssetCreated>(request) with
            {
                AccountId = accountId,
                AssetId = Guid.NewGuid()
            };

            return await _assetService.CreateAsset(evt);
        }

        private async Task<Result<Guid>> HandleLedger(CreateAssetCommand request, Guid accountId)
        {
            var evt = _mapper.Map<LedgerAssetCreated>(request) with
            {
                AccountId = accountId,
                AssetId = Guid.NewGuid()
            };

            return await _assetService.CreateAsset(evt);

        }
    }
}