using Microsoft.EntityFrameworkCore;
using work_10.Data;
using work_10.Models;

namespace work_10.Repositories
{
    public class StockReservationRepository : Repository<StockReservation>, IStockReservationRepository
    {
        public StockReservationRepository(ComputerShopContext context) : base(context)
        {
        }

        public async Task<StockReservation?> FindActiveReservationAsync(
            int productId,
            int customerId,
            CancellationToken ct = default)
        {
            return await _dbSet
                .Where(r => r.ProductId == productId
                         && r.CustomerId == customerId
                         && r.IsActive)
                .FirstOrDefaultAsync(ct);
        }

        public async Task<StockReservation?> GetByIdWithProductAsync(
            int id,
            CancellationToken ct = default)
        {
            return await _dbSet
                .Include(r => r.Product)
                .FirstOrDefaultAsync(r => r.IdReservation == id, ct);
        }

        public async Task<List<StockReservation>> GetByProductIdAsync(
            int productId,
            CancellationToken ct = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId)
                .OrderByDescending(r => r.ReservedAt)
                .ToListAsync(ct);
        }

        public async Task<bool> ProductExistsAsync(int productId, CancellationToken ct = default)
        {
            return await _context.Products
                .AsNoTracking()
                .AnyAsync(p => p.IdProduct == productId, ct);
        }

        public async Task<bool> CustomerExistsAsync(int customerId, CancellationToken ct = default)
        {
            return await _context.Customers
                .AsNoTracking()
                .AnyAsync(c => c.IdCustomer == customerId, ct);
        }
    }
}