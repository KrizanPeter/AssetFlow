using AssetFlow.Application.Interfaces.IRepositories;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities.DocumentEntities;
using Marten;

namespace AssetFlow.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDocumentSession _session;

        public UnitOfWork(IDocumentSession session,
            IDocumentRepositoryDb<Account> accountRepository,
            IEventRepository eventRepository)
        {
            _session = session;
            Accounts = accountRepository;
            Events = eventRepository;
        }

        public IDocumentRepositoryDb<Account> Accounts { get; }
        public IEventRepository Events {get;}


        public Task CommitAsync() => _session.SaveChangesAsync();
    }
}
