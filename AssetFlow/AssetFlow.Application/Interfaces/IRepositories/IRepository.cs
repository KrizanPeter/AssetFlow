using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AssetFlow.Application.Interfaces.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Remove(T entity);
        Task<IEnumerable<T?>> GetAllAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string? includeProperties = null
        );
        Task<T?> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? filter = null);
    }
}
