namespace work_10.Dtos.Auth
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string RoleName { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }
}