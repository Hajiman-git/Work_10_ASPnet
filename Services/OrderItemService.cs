using AutoMapper;
using work_10.Dtos.OrderItem;
using work_10.Models;
using work_10.Repositories;

namespace work_10.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> _repository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public OrderItemService(
            IRepository<OrderItem> repository,
            IRepository<Order> orderRepository,
            IRepository<Product> productRepository,
            IMapper mapper)
        {
            _repository = repository;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<(List<OrderItemDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var (items, total) = await _repository.GetPagedAsync(page, pageSize, ct);
            var dtos = _mapper.Map<List<OrderItemDto>>(items);
            return (dtos, total);
        }

        public async Task<OrderItemDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var item = await _repository.GetByIdAsync(id, ct);
            return item is null ? null : _mapper.Map<OrderItemDto>(item);
        }

        public async Task<OrderItemDto> CreateAsync(
            CreateOrderItemDto dto,
            CancellationToken ct = default)
        {
            var orderExists = await _orderRepository.ExistsAsync(dto.OrderId, ct);
            if (!orderExists)
                throw new InvalidOperationException(
                    $"Заказ с Id {dto.OrderId} не найден.");

            var productExists = await _productRepository.ExistsAsync(dto.ProductId, ct);
            if (!productExists)
                throw new InvalidOperationException(
                    $"Товар с Id {dto.ProductId} не найден.");

            var item = _mapper.Map<OrderItem>(dto);

            await _repository.AddAsync(item, ct);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<OrderItemDto>(item);
        }

        public async Task<OrderItemDto?> UpdateAsync(
            int id,
            UpdateOrderItemDto dto,
            CancellationToken ct = default)
        {
            var item = await _repository.GetByIdAsync(id, ct);
            if (item is null)
                return null;

            var orderExists = await _orderRepository.ExistsAsync(dto.OrderId, ct);
            if (!orderExists)
                throw new InvalidOperationException(
                    $"Заказ с Id {dto.OrderId} не найден.");

            var productExists = await _productRepository.ExistsAsync(dto.ProductId, ct);
            if (!productExists)
                throw new InvalidOperationException(
                    $"Товар с Id {dto.ProductId} не найден.");

            _mapper.Map(dto, item);
            _repository.Update(item);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<OrderItemDto>(item);
        }

        public async Task<OrderItemDto?> PatchAsync(
            int id,
            PatchOrderItemDto dto,
            CancellationToken ct = default)
        {
            var item = await _repository.GetByIdAsync(id, ct);
            if (item is null)
                return null;

            if (dto.OrderId is not null)
            {
                var orderExists = await _orderRepository.ExistsAsync(dto.OrderId.Value, ct);
                if (!orderExists)
                    throw new InvalidOperationException(
                        $"Заказ с Id {dto.OrderId.Value} не найден.");

                item.OrderId = dto.OrderId.Value;
            }

            if (dto.ProductId is not null)
            {
                var productExists = await _productRepository
                    .ExistsAsync(dto.ProductId.Value, ct);
                if (!productExists)
                    throw new InvalidOperationException(
                        $"Товар с Id {dto.ProductId.Value} не найден.");

                item.ProductId = dto.ProductId.Value;
            }

            if (dto.Quantity is not null)
                item.Quantity = dto.Quantity.Value;

            if (dto.PriceAtTime is not null)
                item.PriceAtTime = dto.PriceAtTime.Value;

            _repository.Update(item);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<OrderItemDto>(item);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var item = await _repository.GetByIdAsync(id, ct);
            if (item is null)
                return false;

            _repository.Remove(item);
            await _repository.SaveChangesAsync(ct);
            return true;
        }
    }
}