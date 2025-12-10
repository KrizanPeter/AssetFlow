using AssetFlow.Application.Interfaces.IRepositories;
using AssetFlow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.Interfaces.IServices
{
    public interface IUnitOfWork
    {
        IDocumentRepositoryDb<Account> Accounts { get; }
        Task CommitAsync();

    }
}
