using AutoMapper;
using work_10.Dtos.Category;
using work_10.Models;
using work_10.Repositories;

namespace work_10.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(List<CategoryDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var (items, total) = await _repository.GetPagedAsync(page, pageSize, ct);
            var dtos = _mapper.Map<List<CategoryDto>>(items);
            return (dtos, total);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var category = await _repository.GetByIdAsync(id, ct);
            return category is null ? null : _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(
            CreateCategoryDto dto,
            CancellationToken ct = default)
        {
            var category = _mapper.Map<Category>(dto);
            await _repository.AddAsync(category, ct);
            await _repository.SaveChangesAsync(ct);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto?> UpdateAsync(
            int id,
            UpdateCategoryDto dto,
            CancellationToken ct = default)
        {
            var category = await _repository.GetByIdAsync(id, ct);
            if (category is null)
                return null;

            _mapper.Map(dto, category);
            _repository.Update(category);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto?> PatchAsync(
            int id,
            PatchCategoryDto dto,
            CancellationToken ct = default)
        {
            var category = await _repository.GetByIdAsync(id, ct);
            if (category is null)
                return null;

            if (dto.CategoryName is not null)
                category.CategoryName = dto.CategoryName;

            if (dto.Description is not null)
                category.Description = dto.Description;

            _repository.Update(category);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var category = await _repository.GetByIdAsync(id, ct);
            if (category is null)
                return false;

            _repository.Remove(category);
            await _repository.SaveChangesAsync(ct);
            return true;
        }
    }
}