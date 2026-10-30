using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.Auth
{
    public class RegisterDto
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; } = null!;

        [Required]
        [RegularExpression(@"^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$",
            ErrorMessage = "Телефон должен быть в формате +7(XXX)XXX-XX-XX")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FullName { get; set; } = null!;

        public int RoleId { get; set; } = 2;
    }
}