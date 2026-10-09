using AutoMapper;
using work_10.Dtos.CartItem;
using work_10.Models;
using work_10.Repositories;

namespace work_10.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly IRepository<CartItem> _repository;
        private readonly IRepository<Cart> _cartRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IMapper _mapper;

        public CartItemService(
            IRepository<CartItem> repository,
            IRepository<Cart> cartRepository,
            IRepository<Product> productRepository,
            IMapper mapper)
        {
            _repository = repository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<(List<CartItemDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var (items, total) = await _repository.GetPagedAsync(page, pageSize, ct);
            var dtos = _mapper.Map<List<CartItemDto>>(items);
            return (dtos, total);
        }

        public async Task<CartItemDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var item = await _repository.GetByIdAsync(id, ct);
            return item is null ? null : _mapper.Map<CartItemDto>(item);
        }

        public async Task<CartItemDto> CreateAsync(
            CreateCartItemDto dto,
            CancellationToken ct = default)
        {
            var cartExists = await _cartRepository.ExistsAsync(dto.CartId, ct);
            if (!cartExists)
                throw new InvalidOperationException(
                    $"Корзина с Id {dto.CartId} не найдена.");

            var productExists = await _productRepository.ExistsAsync(dto.ProductId, ct);
            if (!productExists)
                throw new InvalidOperationException(
                    $"Товар с Id {dto.ProductId} не найден.");

            var item = _mapper.Map<CartItem>(dto);
            item.AddedDate = DateTime.UtcNow;

            await _repository.AddAsync(item, ct);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<CartItemDto>(item);
        }

        public async Task<CartItemDto?> UpdateAsync(
            int id,
            UpdateCartItemDto dto,
            CancellationToken ct = default)
        {
            var item = await _repository.GetByIdAsync(id, ct);
            if (item is null)
                return null;

            var cartExists = await _cartRepository.ExistsAsync(dto.CartId, ct);
            if (!cartExists)
                throw new InvalidOperationException(
                    $"Корзина с Id {dto.CartId} не найдена.");

            var productExists = await _productRepository.ExistsAsync(dto.ProductId, ct);
            if (!productExists)
                throw new InvalidOperationException(
                    $"Товар с Id {dto.ProductId} не найден.");

            _mapper.Map(dto, item);
            _repository.Update(item);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<CartItemDto>(item);
        }

        public async Task<CartItemDto?> PatchAsync(
            int id,
            PatchCartItemDto dto,
            CancellationToken ct = default)
        {
            var item = await _repository.GetByIdAsync(id, ct);
            if (item is null)
                return null;

            if (dto.CartId is not null)
            {
                var cartExists = await _cartRepository.ExistsAsync(dto.CartId.Value, ct);
                if (!cartExists)
                    throw new InvalidOperationException(
                        $"Корзина с Id {dto.CartId.Value} не найдена.");

                item.CartId = dto.CartId.Value;
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

            _repository.Update(item);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<CartItemDto>(item);
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