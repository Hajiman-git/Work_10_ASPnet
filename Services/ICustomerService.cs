using work_10.Dtos.Customer;

namespace work_10.Services
{
    public interface ICustomerService
    {
        Task<(List<CustomerDto> Items, int TotalCount)> GetAllAsync(
            int page,
            int pageSize,
            CancellationToken ct = default);

        Task<CustomerDto?> GetByIdAsync(int id, CancellationToken ct = default);

        Task<CustomerDto> CreateAsync(CreateCustomerDto dto, CancellationToken ct = default);

        Task<CustomerDto?> UpdateAsync(
            int id,
            UpdateCustomerDto dto,
            CancellationToken ct = default);

        Task<CustomerDto?> PatchAsync(
            int id,
            PatchCustomerDto dto,
            CancellationToken ct = default);

        Task<bool> DeleteAsync(int id, CancellationToken ct = default);
    }
}