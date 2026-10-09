using AutoMapper;
using work_10.Dtos.Cart;
using work_10.Models;
using work_10.Repositories;

namespace work_10.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _repository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CartService(
            IRepository<Cart> repository,
            IRepository<Customer> customerRepository,
            IMapper mapper)
        {
            _repository = repository;
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<(List<CartDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var (items, total) = await _repository.GetPagedAsync(page, pageSize, ct);
            var dtos = _mapper.Map<List<CartDto>>(items);
            return (dtos, total);
        }

        public async Task<CartDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var cart = await _repository.GetByIdAsync(id, ct);
            return cart is null ? null : _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto> CreateAsync(CreateCartDto dto, CancellationToken ct = default)
        {
            var customerExists = await _customerRepository.ExistsAsync(dto.CustomerId, ct);
            if (!customerExists)
                throw new InvalidOperationException(
                    $"Клиент с Id {dto.CustomerId} не найден.");

            var cart = _mapper.Map<Cart>(dto);
            cart.CreatedDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _repository.AddAsync(cart, ct);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto?> UpdateAsync(
            int id,
            UpdateCartDto dto,
            CancellationToken ct = default)
        {
            var cart = await _repository.GetByIdAsync(id, ct);
            if (cart is null)
                return null;

            var customerExists = await _customerRepository.ExistsAsync(dto.CustomerId, ct);
            if (!customerExists)
                throw new InvalidOperationException(
                    $"Клиент с Id {dto.CustomerId} не найден.");

            _mapper.Map(dto, cart);
            _repository.Update(cart);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<CartDto?> PatchAsync(
            int id,
            PatchCartDto dto,
            CancellationToken ct = default)
        {
            var cart = await _repository.GetByIdAsync(id, ct);
            if (cart is null)
                return null;

            if (dto.CustomerId is not null)
            {
                var customerExists = await _customerRepository
                    .ExistsAsync(dto.CustomerId.Value, ct);
                if (!customerExists)
                    throw new InvalidOperationException(
                        $"Клиент с Id {dto.CustomerId.Value} не найден.");

                cart.CustomerId = dto.CustomerId.Value;
            }

            _repository.Update(cart);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<CartDto>(cart);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var cart = await _repository.GetByIdAsync(id, ct);
            if (cart is null)
                return false;

            _repository.Remove(cart);
            await _repository.SaveChangesAsync(ct);
            return true;
        }
    }
}