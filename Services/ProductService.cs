using AutoMapper;
using work_10.Dtos.Product;
using work_10.Models;
using work_10.Repositories;

namespace work_10.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(List<ProductDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var (items, total) = await _repository.GetPagedWithCategoryAsync(page, pageSize, ct);

            var dtos = _mapper.Map<List<ProductDto>>(items);

            return (dtos, total);
        }

        public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var product = await _repository.GetByIdWithCategoryAsync(id, ct);

            if (product is null)
                return null;

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateAsync(
            CreateProductDto dto,
            CancellationToken ct = default)
        {
            var categoryExists = await _repository.CategoryExistsAsync(dto.CategoryId, ct);
            if (!categoryExists)
                throw new InvalidOperationException(
                    $"Категория с Id {dto.CategoryId} не найдена.");

            var product = _mapper.Map<Product>(dto);

            await _repository.AddAsync(product, ct);
            await _repository.SaveChangesAsync(ct);

            var saved = await _repository.GetByIdWithCategoryAsync(product.IdProduct, ct)
                ?? throw new InvalidOperationException(
                    "Не удалось загрузить только что созданный товар.");

            return _mapper.Map<ProductDto>(saved);
        }

        public async Task<ProductDto?> UpdateAsync(
            int id,
            UpdateProductDto dto,
            CancellationToken ct = default)
        {
            var product = await _repository.GetByIdAsync(id, ct);
            if (product is null)
                return null;

            var categoryExists = await _repository.CategoryExistsAsync(dto.CategoryId, ct);
            if (!categoryExists)
                throw new InvalidOperationException(
                    $"Категория с Id {dto.CategoryId} не найдена.");

            _mapper.Map(dto, product);

            _repository.Update(product);
            await _repository.SaveChangesAsync(ct);

            var updated = await _repository.GetByIdWithCategoryAsync(id, ct);

            return updated is null ? null : _mapper.Map<ProductDto>(updated);
        }

        public async Task<ProductDto?> PatchAsync(
            int id,
            PatchProductDto dto,
            CancellationToken ct = default)
        {
            var product = await _repository.GetByIdAsync(id, ct);
            if (product is null)
                return null;

            if (dto.ProductName is not null)
                product.ProductName = dto.ProductName;

            if (dto.CategoryId is not null)
            {
                var categoryExists = await _repository.CategoryExistsAsync(dto.CategoryId.Value, ct);
                if (!categoryExists)
                    throw new InvalidOperationException(
                        $"Категория с Id {dto.CategoryId.Value} не найдена.");

                product.CategoryId = dto.CategoryId.Value;
            }

            if (dto.Price is not null)
                product.Price = dto.Price.Value;

            if (dto.StockQuantity is not null)
                product.StockQuantity = dto.StockQuantity.Value;

            if (dto.Description is not null)
                product.Description = dto.Description;

            if (dto.IsService is not null)
                product.IsService = dto.IsService.Value;

            if (dto.ImageUrl is not null)
                product.ImageUrl = dto.ImageUrl;

            _repository.Update(product);
            await _repository.SaveChangesAsync(ct);

            var updated = await _repository.GetByIdWithCategoryAsync(id, ct);

            return updated is null ? null : _mapper.Map<ProductDto>(updated);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var product = await _repository.GetByIdAsync(id, ct);
            if (product is null)
                return false;

            _repository.Remove(product);
            await _repository.SaveChangesAsync(ct);

            return true;
        }
    }
}