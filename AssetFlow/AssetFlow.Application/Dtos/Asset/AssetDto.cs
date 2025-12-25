
namespace AssetFlow.Application.Dtos.Asset
{
    public class AssetDto
    {
        public Guid Id { get; set; }
        public string AssetName { get; set; } = string.Empty;
        public AssetType TypeOfAsset { get; set; }
        public string UnitType { get; set; } = string.Empty;
        public decimal Balance { get; set; }

        public List<SnapshotDto> Snapshots { get; set; } = new List<SnapshotDto>();  

    }
}
