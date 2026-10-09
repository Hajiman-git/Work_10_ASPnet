using AutoMapper;
using work_10.Dtos.Customer;
using work_10.Models;
using work_10.Repositories;

namespace work_10.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _repository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public CustomerService(
            IRepository<Customer> repository,
            IRepository<Role> roleRepository,
            IMapper mapper)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<(List<CustomerDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default)
        {
            var (items, total) = await _repository.GetPagedAsync(page, pageSize, ct);
            var dtos = _mapper.Map<List<CustomerDto>>(items);
            return (dtos, total);
        }

        public async Task<CustomerDto?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            var customer = await _repository.GetByIdAsync(id, ct);
            return customer is null ? null : _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> CreateAsync(
            CreateCustomerDto dto,
            CancellationToken ct = default)
        {
            var roleExists = await _roleRepository.ExistsAsync(dto.RoleId, ct);
            if (!roleExists)
                throw new InvalidOperationException(
                    $"Роль с Id {dto.RoleId} не найдена.");

            var customer = _mapper.Map<Customer>(dto);
            customer.RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow);

            await _repository.AddAsync(customer, ct);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto?> UpdateAsync(
            int id,
            UpdateCustomerDto dto,
            CancellationToken ct = default)
        {
            var customer = await _repository.GetByIdAsync(id, ct);
            if (customer is null)
                return null;

            var roleExists = await _roleRepository.ExistsAsync(dto.RoleId, ct);
            if (!roleExists)
                throw new InvalidOperationException(
                    $"Роль с Id {dto.RoleId} не найдена.");

            _mapper.Map(dto, customer);
            _repository.Update(customer);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto?> PatchAsync(
            int id,
            PatchCustomerDto dto,
            CancellationToken ct = default)
        {
            var customer = await _repository.GetByIdAsync(id, ct);
            if (customer is null)
                return null;

            if (dto.Username is not null)
                customer.Username = dto.Username;

            if (dto.PasswordHash is not null)
                customer.PasswordHash = dto.PasswordHash;

            if (dto.Email is not null)
                customer.Email = dto.Email;

            if (dto.PhoneNumber is not null)
                customer.PhoneNumber = dto.PhoneNumber;

            if (dto.FullName is not null)
                customer.FullName = dto.FullName;

            if (dto.RoleId is not null)
            {
                var roleExists = await _roleRepository.ExistsAsync(dto.RoleId.Value, ct);
                if (!roleExists)
                    throw new InvalidOperationException(
                        $"Роль с Id {dto.RoleId.Value} не найдена.");

                customer.RoleId = dto.RoleId.Value;
            }

            if (dto.IsActive is not null)
                customer.IsActive = dto.IsActive.Value;

            _repository.Update(customer);
            await _repository.SaveChangesAsync(ct);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken ct = default)
        {
            var customer = await _repository.GetByIdAsync(id, ct);
            if (customer is null)
                return false;

            _repository.Remove(customer);
            await _repository.SaveChangesAsync(ct);
            return true;
        }
    }
}