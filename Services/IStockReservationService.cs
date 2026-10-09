using work_10.Dtos.StockReservation;

namespace work_10.Services
{
    public interface IStockReservationService
    {

        Task<StockReservationDto> ReserveAsync(
            int productId,
            ReserveStockDto dto,
            CancellationToken ct = default);


        Task<bool> ReleaseAsync(
            int productId,
            int customerId,
            CancellationToken ct = default);

        Task<(List<StockReservationDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<StockReservationDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<List<StockReservationDto>> GetByProductIdAsync(
            int productId,
            CancellationToken ct = default);
    }
}