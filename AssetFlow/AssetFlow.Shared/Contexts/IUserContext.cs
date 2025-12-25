using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Shared.Contexts
{
    public interface IUserContext
    {
        Guid UserId { get; set; }
        Guid AccountId { get; set; }
    }
}
