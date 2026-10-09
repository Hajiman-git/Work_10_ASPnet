using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using work_10.Data;

namespace work_10.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ComputerShopContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(ComputerShopContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbSet.AsNoTracking().ToListAsync(ct);
        }

        public async Task<(List<T> Items, int TotalCount)> GetPagedAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _dbSet.AsNoTracking();

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, totalCount);
        }

        public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, ct);
        }

        public async Task<List<T>> FindAsync(
            Expression<Func<T, bool>> predicate,
            CancellationToken ct = default)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync(ct);
        }

        public async Task<int> CountAsync(CancellationToken ct = default)
        {
            return await _dbSet.CountAsync(ct);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, ct) is not null;
        }

        public async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _context.SaveChangesAsync(ct);
        }
    }
}