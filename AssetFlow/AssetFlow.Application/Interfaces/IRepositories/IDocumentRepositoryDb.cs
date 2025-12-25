
using Marten;

namespace AssetFlow.Application.Interfaces.IRepositories
{
    public interface IDocumentRepositoryDb<T> 
    {
        Task<T?> GetByIdAsync(Guid id);
        Task AddAsync(T document);
        Task UpdateAsync(T document);
        Task DeleteAsync(Guid id);
        void WithSession(IDocumentSession session);

    }
}
