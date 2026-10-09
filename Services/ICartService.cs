using work_10.Dtos.Cart;

namespace work_10.Services
{
    public interface ICartService
    {
        Task<(List<CartDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<CartDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<CartDto> CreateAsync(CreateCartDto dto, CancellationToken ct = default);

        Task<CartDto?> UpdateAsync(int id, UpdateCartDto dto, CancellationToken ct = default);

        Task<CartDto?> PatchAsync(int id, PatchCartDto dto, CancellationToken ct = default);

        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}