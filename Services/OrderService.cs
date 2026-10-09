using AutoMapper;
using work_10.Dtos.Order;
using work_10.Models;
using work_10.Repositories;

namespace work_10.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _repository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<OrderStatus> _statusRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IRepository<Order> repository,
            IRepository<Customer> customerRepository,
            IRepository<OrderStatus> statusRepository,
            IMapper mapper)
        {
            _repository = repository;
            _customerRepository = customerRepository;
            _statusRepository = statusRepository;
            _mapper = mapper;
        }

        public async Task<(List<OrderDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var (items, total) = await _repository.GetPagedAsync(page, pageSize, ct);
            var dtos = _mapper.Map<List<OrderDto>>(items);
            return (dtos, total);
        }

        public async Task<OrderDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var order = await _repository.GetByIdAsync(id, ct);
            return order is null ? null : _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto dto, CancellationToken ct = default)
        {
            var customerExists = await _customerRepository.ExistsAsync(dto.CustomerId, ct);
            if (!customerExists)
                throw new InvalidOperationException(
                    $"Клиент с Id {dto.CustomerId} не найден.");

            var statusExists = await _statusRepository.ExistsAsync(dto.StatusId, ct);
            if (!statusExists)
                throw new InvalidOperationException(
                    $"Статус заказа с Id {dto.StatusId} не найден.");

            var order = _mapper.Map<Order>(dto);
            order.OrderDate = DateTime.UtcNow;

            await _repository.AddAsync(order, ct);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> UpdateAsync(
            int id,
            UpdateOrderDto dto,
            CancellationToken ct = default)
        {
            var order = await _repository.GetByIdAsync(id, ct);
            if (order is null)
                return null;

            var customerExists = await _customerRepository.ExistsAsync(dto.CustomerId, ct);
            if (!customerExists)
                throw new InvalidOperationException(
                    $"Клиент с Id {dto.CustomerId} не найден.");

            var statusExists = await _statusRepository.ExistsAsync(dto.StatusId, ct);
            if (!statusExists)
                throw new InvalidOperationException(
                    $"Статус заказа с Id {dto.StatusId} не найден.");

            _mapper.Map(dto, order);
            _repository.Update(order);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<OrderDto?> PatchAsync(
            int id,
            PatchOrderDto dto,
            CancellationToken ct = default)
        {
            var order = await _repository.GetByIdAsync(id, ct);
            if (order is null)
                return null;

            if (dto.CustomerId is not null)
            {
                var customerExists = await _customerRepository
                    .ExistsAsync(dto.CustomerId.Value, ct);
                if (!customerExists)
                    throw new InvalidOperationException(
                        $"Клиент с Id {dto.CustomerId.Value} не найден.");

                order.CustomerId = dto.CustomerId.Value;
            }

            if (dto.StatusId is not null)
            {
                var statusExists = await _statusRepository
                    .ExistsAsync(dto.StatusId.Value, ct);
                if (!statusExists)
                    throw new InvalidOperationException(
                        $"Статус заказа с Id {dto.StatusId.Value} не найден.");

                order.StatusId = dto.StatusId.Value;
            }

            if (dto.TotalAmount is not null)
                order.TotalAmount = dto.TotalAmount.Value;

            if (dto.DeliveryAddress is not null)
                order.DeliveryAddress = dto.DeliveryAddress;

            if (dto.Comment is not null)
                order.Comment = dto.Comment;

            _repository.Update(order);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<OrderDto>(order);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var order = await _repository.GetByIdAsync(id, ct);
            if (order is null)
                return false;

            _repository.Remove(order);
            await _repository.SaveChangesAsync(ct);
            return true;
        }
    }
}