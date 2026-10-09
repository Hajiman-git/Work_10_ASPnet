using work_10.Dtos.OrderItem;

namespace work_10.Services
{
    public interface IOrderItemService
    {
        Task<(List<OrderItemDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<OrderItemDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<OrderItemDto> CreateAsync(
            CreateOrderItemDto dto,
            CancellationToken ct = default);

        Task<OrderItemDto?> UpdateAsync(
            int id,
            UpdateOrderItemDto dto,
            CancellationToken ct = default);

        Task<OrderItemDto?> PatchAsync(
            int id,
            PatchOrderItemDto dto,
            CancellationToken ct = default);

        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}