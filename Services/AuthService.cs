using Microsoft.AspNetCore.Identity;
using work_10.Dtos.Auth;
using work_10.Models;
using work_10.Repositories;

namespace work_10.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly ITokenService _tokenService;
        private readonly PasswordHasher<Customer> _passwordHasher;

        public AuthService(
            IRepository<Customer> customerRepository,
            IRepository<Role> roleRepository,
            ITokenService tokenService)
        {
            _customerRepository = customerRepository;
            _roleRepository = roleRepository;
            _tokenService = tokenService;
            _passwordHasher = new PasswordHasher<Customer>();
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto, CancellationToken ct = default)
        {
            var existing = await _customerRepository.FindAsync(c => c.Username == dto.Username, ct);
            if (existing.Any())
                throw new InvalidOperationException("Пользователь с таким логином уже существует.");

            var role = await _roleRepository.GetByIdAsync(dto.RoleId, ct);
            if (role is null)
                throw new InvalidOperationException($"Роль с Id {dto.RoleId} не найдена.");

            var customer = new Customer
            {
                Username = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                FullName = dto.FullName,
                RoleId = dto.RoleId,
                RegistrationDate = DateOnly.FromDateTime(DateTime.UtcNow),
                IsActive = true
            };

            customer.PasswordHash = _passwordHasher.HashPassword(customer, dto.Password);

            await _customerRepository.AddAsync(customer, ct);
            await _customerRepository.SaveChangesAsync(ct);

            var (token, expiresAt) = _tokenService.GenerateToken(customer, role.RoleName);

            return new AuthResponseDto
            {
                Token = token,
                UserId = customer.IdCustomer,
                Username = customer.Username,
                RoleName = role.RoleName,
                ExpiresAt = expiresAt
            };
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto, CancellationToken ct = default)
        {
            var customers = await _customerRepository.FindAsync(c => c.Username == dto.Username, ct);
            var customer = customers.FirstOrDefault();

            if (customer is null || !customer.IsActive)
                throw new InvalidOperationException("Неверный логин или пароль, либо пользователь заблокирован.");

            var verificationResult = _passwordHasher.VerifyHashedPassword(
                customer, customer.PasswordHash, dto.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
                throw new InvalidOperationException("Неверный логин или пароль.");

            var role = await _roleRepository.GetByIdAsync(customer.RoleId, ct);
            if (role is null)
                throw new InvalidOperationException("Роль пользователя не найдена.");

            var (token, expiresAt) = _tokenService.GenerateToken(customer, role.RoleName);

            return new AuthResponseDto
            {
                Token = token,
                UserId = customer.IdCustomer,
                Username = customer.Username,
                RoleName = role.RoleName,
                ExpiresAt = expiresAt
            };
        }
    }
}