using AssetFlow.Application.Interfaces.IRepositories;
using AssetFlow.Application.Interfaces.IServices;
using AssetFlow.Domain.Entities.DocumentEntities;
using Marten;

namespace AssetFlow.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDocumentSession _session;
        public IQuerySession QuerySession { get; set; }

        public UnitOfWork(IDocumentSession session, 
            IQuerySession querySession,
            IDocumentRepositoryDb<Account> accountRepository,
            IEventRepository eventRepository)
        {
            _session = session;
            QuerySession = querySession;
            Accounts = accountRepository;
            Events = eventRepository;

            //Setting Same session 
            Events.WithSession(session);
            Accounts.WithSession(session);
        }

        public IDocumentRepositoryDb<Account> Accounts { get; }
        public IEventRepository Events {get;}


        public Task CommitAsync() => _session.SaveChangesAsync();
    }
}
