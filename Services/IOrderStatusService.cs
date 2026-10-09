using work_10.Dtos.OrderStatus;

namespace work_10.Services
{
    public interface IOrderStatusService
    {
        Task<(List<OrderStatusDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<OrderStatusDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<OrderStatusDto> CreateAsync(
            CreateOrderStatusDto dto,
            CancellationToken ct = default);

        Task<OrderStatusDto?> UpdateAsync(
            int id,
            UpdateOrderStatusDto dto,
            CancellationToken ct = default);

        Task<OrderStatusDto?> PatchAsync(
            int id,
            PatchOrderStatusDto dto,
            CancellationToken ct = default);

        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}