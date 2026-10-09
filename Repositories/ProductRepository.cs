using Microsoft.EntityFrameworkCore;
using work_10.Data;
using work_10.Models;

namespace work_10.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(ComputerShopContext context) : base(context)
        {
        }

        public async Task<(List<Product> Items, int TotalCount)> GetPagedWithCategoryAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var query = _dbSet
                .AsNoTracking()
                .Include(p => p.Category);

            var totalCount = await query.CountAsync(ct);

            var items = await query
                .OrderBy(p => p.IdProduct)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (items, totalCount);
        }

        public async Task<Product?> GetByIdWithCategoryAsync(int id, CancellationToken ct = default)
        {
            return await _dbSet
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.IdProduct == id, ct);
        }

        public async Task<bool> CategoryExistsAsync(int categoryId, CancellationToken ct = default)
        {
            return await _context.Categories
                .AsNoTracking()
                .AnyAsync(c => c.IdCategory == categoryId, ct);
        }
    }
}