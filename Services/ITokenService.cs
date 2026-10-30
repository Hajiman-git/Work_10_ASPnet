using work_10.Models;

namespace work_10.Services
{
    public interface ITokenService
    {
        (string Token, DateTime ExpiresAt) GenerateToken(Customer customer, string roleName);
    }
}