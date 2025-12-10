using AssetFlow.Application.Interfaces.IRepositories;
using Marten;

namespace AssetFlow.Persistence.Repositories
{
    public class DocumentRepositoryDb<T> : IDocumentRepositoryDb<T> where T : class
    {
        private readonly IDocumentSession _session;

        public DocumentRepositoryDb(IDocumentSession session)
        {
            _session = session;
        }

        public Task<T?> GetByIdAsync(Guid id) =>
            _session.LoadAsync<T>(id);

        // Immediate commit methods
        public async Task AddAsync(T document)
        {
            _session.Store(document);
            await _session.SaveChangesAsync();
        }

        public async Task UpdateAsync(T document)
        {
            _session.Store(document);
            await _session.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _session.LoadAsync<T>(id);
            if (entity != null)
            {
                _session.Delete(entity);
                await _session.SaveChangesAsync();
            }
        }

        // Transactional equivalents (queue only, no commit yet)
        public Task TransactionalAddAsync(T document)
        {
            _session.Store(document);
            return Task.CompletedTask;
        }

        public Task TransactionalUpdateAsync(T document)
        {
            _session.Store(document);
            return Task.CompletedTask;
        }

        public async Task TransactionalDeleteAsync(Guid id)
        {
            var entity = await _session.LoadAsync<T>(id);
            if (entity != null)
            {
                _session.Delete(entity);
            }
        }

        // Commit all queued transactional operations
        public Task CommitAsync() => _session.SaveChangesAsync();
    }
}
