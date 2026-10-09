using work_10.Dtos.Category;

namespace work_10.Services
{
    public interface ICategoryService
    {
        Task<(List<CategoryDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<CategoryDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<CategoryDto> CreateAsync(CreateCategoryDto dto, CancellationToken ct = default);

        Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryDto dto, CancellationToken ct = default);

        Task<CategoryDto?> PatchAsync(int id, PatchCategoryDto dto, CancellationToken ct = default);

        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}