using work_10.Dtos.Order;

namespace work_10.Services
{
    public interface IOrderService
    {
        Task<(List<OrderDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<OrderDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<OrderDto> CreateAsync(CreateOrderDto dto, CancellationToken ct = default);

        Task<OrderDto?> UpdateAsync(int id, UpdateOrderDto dto, CancellationToken ct = default);

        Task<OrderDto?> PatchAsync(int id, PatchOrderDto dto, CancellationToken ct = default);

        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}