using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Domain.Events
{
    public record SnapshotCreated(Guid Id, DateTime CreatedAt, decimal Balance);

}
