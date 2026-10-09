using work_10.Dtos.Role;

namespace work_10.Services
{
    public interface IRoleService
    {
        Task<(List<RoleDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<RoleDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<RoleDto> CreateAsync(CreateRoleDto dto, CancellationToken ct = default);

        Task<RoleDto?> UpdateAsync(int id, UpdateRoleDto dto, CancellationToken ct = default);

        Task<RoleDto?> PatchAsync(int id, PatchRoleDto dto, CancellationToken ct = default);

        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}