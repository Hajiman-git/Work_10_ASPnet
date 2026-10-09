using AutoMapper;
using work_10.Dtos.Role;
using work_10.Models;
using work_10.Repositories;

namespace work_10.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _repository;
        private readonly IMapper _mapper;

        public RoleService(IRepository<Role> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<(List<RoleDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var (items, total) = await _repository.GetPagedAsync(page, pageSize, ct);
            var dtos = _mapper.Map<List<RoleDto>>(items);
            return (dtos, total);
        }

        public async Task<RoleDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var role = await _repository.GetByIdAsync(id, ct);
            return role is null ? null : _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto> CreateAsync(CreateRoleDto dto, CancellationToken ct = default)
        {
            var role = _mapper.Map<Role>(dto);
            await _repository.AddAsync(role, ct);
            await _repository.SaveChangesAsync(ct);
            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto?> UpdateAsync(
            int id,
            UpdateRoleDto dto,
            CancellationToken ct = default)
        {
            var role = await _repository.GetByIdAsync(id, ct);
            if (role is null)
                return null;

            _mapper.Map(dto, role);
            _repository.Update(role);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<RoleDto?> PatchAsync(
            int id,
            PatchRoleDto dto,
            CancellationToken ct = default)
        {
            var role = await _repository.GetByIdAsync(id, ct);
            if (role is null)
                return null;

            if (dto.RoleName is not null)
                role.RoleName = dto.RoleName;

            _repository.Update(role);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<RoleDto>(role);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var role = await _repository.GetByIdAsync(id, ct);
            if (role is null)
                return false;

            _repository.Remove(role);
            await _repository.SaveChangesAsync(ct);
            return true;
        }
    }
}