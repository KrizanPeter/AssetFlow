using AssetFlow.Application.Dtos.Asset;
using AssetFlow.Application.Interfaces.IServices;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.MediatR.Queries.Handlers
{
    public class GetAssetsQueryHandler : IRequestHandler<GetAssetsQuery, Result<AssetsDto>>
    {
        private IAssetService _assetService;
        private ILogger<GetAssetsQueryHandler> _logger;

        public GetAssetsQueryHandler(IAssetService assetService, ILogger<GetAssetsQueryHandler> logger)
        {
            _assetService = assetService;
            _logger = logger;
        }

        public async Task<Result<AssetsDto>> Handle(GetAssetsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var assetResult = await _assetService.GetAssetsByAccountId(request.AccountId);
                return assetResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting asset with ID {AssetId}", request.AccountId);
                return Result.Fail<AssetsDto>("An error occurred while resolving the GetAssetsQueryHandler");
            }
        }
    }
}
