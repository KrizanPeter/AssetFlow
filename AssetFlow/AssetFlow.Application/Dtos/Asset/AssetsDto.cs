using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.Dtos.Asset
{
    public class AssetsDto
    {
        public List<AssetDto> Assets { get; set; } = new List<AssetDto>();
    }
}
