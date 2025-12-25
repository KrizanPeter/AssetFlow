using AssetFlow.Application.Dtos.Asset;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Application.MediatR.Commands;
using AssetFlow.Domain.Entities.EventAggregates;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.MediatR.Queries.Handlers
{
    public class GetAssetQueryHandler : IRequestHandler<GetAssetQuery, Result<AssetDto>>
    {
        private IAssetService _assetService;
        private ILogger<GetAssetQueryHandler> _logger;

        public GetAssetQueryHandler(IAssetService assetService, ILogger<GetAssetQueryHandler> logger)
        {
            _assetService = assetService;
            _logger = logger;
        }

        public async Task<Result<AssetDto>> Handle(GetAssetQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var assetResult = await _assetService.GetAssetById(request.AssetId);
                return assetResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting asset with ID {AssetId}", request.AssetId);
                return Result.Fail<AssetDto>("An error occurred while resolving the GetAssetQueryHandler");
            }
        }
    }
}
