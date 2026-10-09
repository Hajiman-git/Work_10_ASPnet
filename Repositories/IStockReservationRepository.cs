using work_10.Models;

namespace work_10.Repositories
{
    public interface IStockReservationRepository : IRepository<StockReservation>
    {
        Task<StockReservation?> FindActiveReservationAsync(
            int productId,
            int customerId,
            CancellationToken ct = default);

        Task<StockReservation?> GetByIdWithProductAsync(
            int id,
            CancellationToken ct = default);

        Task<List<StockReservation>> GetByProductIdAsync(
            int productId,
            CancellationToken ct = default);

        Task<bool> ProductExistsAsync(int productId, CancellationToken ct = default);
        Task<bool> CustomerExistsAsync(int customerId, CancellationToken ct = default);
    }
}