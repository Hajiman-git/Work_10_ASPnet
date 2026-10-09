using System.Linq.Expressions;

namespace work_10.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(CancellationToken ct = default);
        Task<(List<T> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);
        Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<List<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken ct = default);
        Task<int> CountAsync(CancellationToken ct = default);
        Task<bool> ExistsAsync(int id, CancellationToken ct = default);

        Task AddAsync(T entity, CancellationToken ct = default);
        void Update(T entity);
        void Remove(T entity);

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}