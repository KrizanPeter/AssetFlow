using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.Dtos.Asset
{
    public class SnapshotDto
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Balance { get; set; } = decimal.Zero;
    }
}
