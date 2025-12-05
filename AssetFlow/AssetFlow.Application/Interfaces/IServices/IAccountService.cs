using FluentResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.Interfaces.IServices
{
    public interface IAccountService
    {
        Task<Result> CreateAccount(Guid userId, Guid accountId);
    }
}
