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
    internal class CreateAssetCommandHandler : IRequestHandler<CreateAssetCommand, Result<AssetDto>>
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

        public async Task<Result<AssetDto>> Handle(CreateAssetCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var accountId = _userContext.AccountId;
                var assetToCreate = _mapper.Map<AssetCreated>(request);

                AssetCreated assetCreatedEvent = assetToCreate with
                {
                    AccountId = accountId,
                    AssetId = Guid.NewGuid()
                };

                return Result.Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }
    }
}