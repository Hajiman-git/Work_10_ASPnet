using AutoMapper;
using work_10.Dtos.OrderStatus;
using work_10.Models;
using work_10.Repositories;

namespace work_10.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IRepository<OrderStatus> _repository;
        private readonly IMapper _mapper;

        public OrderStatusService(IRepository<OrderStatus> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(List<OrderStatusDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var (items, total) = await _repository.GetPagedAsync(page, pageSize, ct);
            var dtos = _mapper.Map<List<OrderStatusDto>>(items);
            return (dtos, total);
        }

        public async Task<OrderStatusDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var status = await _repository.GetByIdAsync(id, ct);
            return status is null ? null : _mapper.Map<OrderStatusDto>(status);
        }

        public async Task<OrderStatusDto> CreateAsync(
            CreateOrderStatusDto dto,
            CancellationToken ct = default)
        {
            var status = _mapper.Map<OrderStatus>(dto);
            await _repository.AddAsync(status, ct);
            await _repository.SaveChangesAsync(ct);
            return _mapper.Map<OrderStatusDto>(status);
        }

        public async Task<OrderStatusDto?> UpdateAsync(
            int id,
            UpdateOrderStatusDto dto,
            CancellationToken ct = default)
        {
            var status = await _repository.GetByIdAsync(id, ct);
            if (status is null)
                return null;

            _mapper.Map(dto, status);
            _repository.Update(status);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<OrderStatusDto>(status);
        }

        public async Task<OrderStatusDto?> PatchAsync(
            int id,
            PatchOrderStatusDto dto,
            CancellationToken ct = default)
        {
            var status = await _repository.GetByIdAsync(id, ct);
            if (status is null)
                return null;

            if (dto.StatusName is not null)
                status.StatusName = dto.StatusName;

            _repository.Update(status);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<OrderStatusDto>(status);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var status = await _repository.GetByIdAsync(id, ct);
            if (status is null)
                return false;

            _repository.Remove(status);
            await _repository.SaveChangesAsync(ct);
            return true;
        }
    }
}