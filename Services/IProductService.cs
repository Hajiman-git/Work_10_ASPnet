using work_10.Dtos.Product;

namespace work_10.Services
{
    public interface IProductService
    {
        Task<(List<ProductDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<ProductDto> CreateAsync(CreateProductDto dto, CancellationToken ct = default);

        Task<ProductDto?> UpdateAsync(int id, UpdateProductDto dto, CancellationToken ct = default);

        Task<ProductDto?> PatchAsync(int id, PatchProductDto dto, CancellationToken ct = default);

        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}