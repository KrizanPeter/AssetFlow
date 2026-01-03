using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Events;
using AssetFlow.Shared.Contexts;
using FluentResults;
using FluentResults.Extensions;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AssetFlow.Application.MediatR.Commands.Handlers
{
    public class AddSnapshotCommandHandler : IRequestHandler<AddSnapshotCommand, Result<Guid>>
    {
        private readonly ILogger<AddSnapshotCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IAssetService _assetService;
        private readonly ISnapshotService _snapshotService;
        private readonly IUserContext _userContext;

        public AddSnapshotCommandHandler(ILogger<AddSnapshotCommandHandler> logger,
            IMapper mapper,
            IAssetService assetService,
            ISnapshotService snapshotService,
            IUserContext userContext)
        {
            _logger = logger;
            _mapper = mapper;
            _assetService = assetService;
            _snapshotService = snapshotService;
            _userContext = userContext;
        }

        public async Task<Result<Guid>> Handle(AddSnapshotCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var accountId = _userContext.AccountId;
                var result = await _assetService.HasOwnership(accountId, request.AssetId)
                    .Bind(_ => MapEvent(request))
                    .Bind(snapshot => _snapshotService.CreateSnapshot(request.AssetId, snapshot));

                return Result.Ok(result.Value);
            }
            catch
            {
                _logger.LogError("Error adding snapshot for asset with ID {AssetId}", request.AssetId);
                return Result.Fail("An error occurred while adding the snapshot.");
            }

        }

        private Result<SnapshotCreated> MapEvent(AddSnapshotCommand request)
        {
            try
            {
                var snapshotEvent = _mapper.Map<SnapshotCreated>(request) with
                {
                    Id = Guid.NewGuid(),
                };

                return Result.Ok(snapshotEvent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while casting SnapshotCreated event");
                return Result.Fail<SnapshotCreated>("Error while mapping SnapshotCreated event");
            }
        }
    }
}
