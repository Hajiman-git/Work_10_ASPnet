using work_10.Dtos.CartItem;

namespace work_10.Services
{
    public interface ICartItemService
    {
        Task<(List<CartItemDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<CartItemDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<CartItemDto> CreateAsync(
            CreateCartItemDto dto,
            CancellationToken ct = default);

        Task<CartItemDto?> UpdateAsync(
            int id,
            UpdateCartItemDto dto,
            CancellationToken ct = default);

        Task<CartItemDto?> PatchAsync(
            int id,
            PatchCartItemDto dto,
            CancellationToken ct = default);

        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}