using work_10.Models;

namespace work_10.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<(List<Product> Items, int TotalCount)> GetPagedWithCategoryAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<Product?> GetByIdWithCategoryAsync(int id, CancellationToken ct = default);

        Task<bool> CategoryExistsAsync(int categoryId, CancellationToken ct = default);
    }
}