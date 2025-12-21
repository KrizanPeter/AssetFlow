using AssetFlow.Application.Interfaces.IRepositories;
using AssetFlow.Domain.Entities.DocumentEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace AssetFlow.Application.Interfaces.IServices
{
    public interface IUnitOfWork
    {
        IDocumentRepositoryDb<Account> Accounts { get; }
        IEventRepository Events { get; }

        Task CommitAsync();

    }
}
