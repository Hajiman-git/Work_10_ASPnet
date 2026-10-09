using AutoMapper;
using work_10.Dtos.StockReservation;
using work_10.Models;
using work_10.Repositories;

namespace work_10.Services
{
    public class StockReservationService : IStockReservationService
    {
        private readonly IStockReservationRepository _reservationRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public StockReservationService(
            IStockReservationRepository reservationRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

       
        public async Task<StockReservationDto> ReserveAsync(
            int productId,
            ReserveStockDto dto,
            CancellationToken ct = default)
        {
            var product = await _productRepository.GetByIdAsync(productId, ct);
            if (product is null)
                throw new InvalidOperationException(
                    $"Товар с Id {productId} не найден.");

            var customerExists = await _reservationRepository
                .CustomerExistsAsync(dto.CustomerId, ct);
            if (!customerExists)
                throw new InvalidOperationException(
                    $"Клиент с Id {dto.CustomerId} не найден.");

            if (product.StockQuantity < dto.Quantity)
                throw new InvalidOperationException(
                    $"Недостаточно товара на складе. " +
                    $"Запрошено: {dto.Quantity}, доступно: {product.StockQuantity}.");

            product.StockQuantity -= dto.Quantity;

            var reservation = new StockReservation
            {
                ProductId = productId,
                CustomerId = dto.CustomerId,
                Quantity = dto.Quantity,
                ReservedAt = DateTime.UtcNow,
                ReleasedAt = null,
                IsActive = true
            };

            await _reservationRepository.AddAsync(reservation, ct);

            await _reservationRepository.SaveChangesAsync(ct);

            var saved = await _reservationRepository
                .GetByIdWithProductAsync(reservation.IdReservation, ct)
                ?? throw new InvalidOperationException(
                    "Не удалось загрузить только что созданную резервацию.");

            return _mapper.Map<StockReservationDto>(saved);
        }

      
        public async Task<bool> ReleaseAsync(
            int productId,
            int customerId,
            CancellationToken ct = default)
        {
            var reservation = await _reservationRepository
                .FindActiveReservationAsync(productId, customerId, ct);

            if (reservation is null)
                return false;

            var product = await _productRepository.GetByIdAsync(productId, ct);
            if (product is null)
                return false;

            product.StockQuantity += reservation.Quantity;

            reservation.IsActive = false;
            reservation.ReleasedAt = DateTime.UtcNow;

            _productRepository.Update(product);
            _reservationRepository.Update(reservation);

            await _reservationRepository.SaveChangesAsync(ct);

            return true;
        }

        public async Task<(List<StockReservationDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var (items, total) = await _reservationRepository
                .GetPagedAsync(page, pageSize, ct);

            var dtos = _mapper.Map<List<StockReservationDto>>(items);

            return (dtos, total);
        }

        public async Task<StockReservationDto?> GetByIdAsync(
            int id,
            CancellationToken ct = default)
        {
            var reservation = await _reservationRepository
                .GetByIdWithProductAsync(id, ct);

            if (reservation is null)
                return null;

            return _mapper.Map<StockReservationDto>(reservation);
        }

        public async Task<List<StockReservationDto>> GetByProductIdAsync(
            int productId,
            CancellationToken ct = default)
        {
            var reservations = await _reservationRepository
                .GetByProductIdAsync(productId, ct);

            return _mapper.Map<List<StockReservationDto>>(reservations);
        }
    }
}