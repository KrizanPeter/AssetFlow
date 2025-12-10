
namespace AssetFlow.Application.Dtos.Asset
{
    public class AssetDto
    {
        public Guid AssetId { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public AssetType TypeOfAsset { get; set; }
        public string UnitType { get; set; } = string.Empty;

    }
}
