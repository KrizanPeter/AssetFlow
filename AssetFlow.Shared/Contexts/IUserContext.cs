using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Shared.Contexts
{
    public interface IUserContext
    {
        string UserId { get; set; }
        string AccountId { get; set; }
    }
}
