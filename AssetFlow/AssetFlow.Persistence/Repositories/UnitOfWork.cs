using AssetFlow.Application.Interfaces.IRepositories;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities;
using Marten;

namespace AssetFlow.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDocumentSession _session;

        public UnitOfWork(IDocumentSession session, IDocumentRepositoryDb<Account> accountRepository)
        {
            _session = session;
            Accounts = accountRepository;
        }

        public IDocumentRepositoryDb<Account> Accounts { get; }


        public Task CommitAsync() => _session.SaveChangesAsync();
    }
}
