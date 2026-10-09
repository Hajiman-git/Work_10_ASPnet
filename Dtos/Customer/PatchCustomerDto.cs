using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.Customer
{
    public class PatchCustomerDto
    {
        [StringLength(30, MinimumLength = 3)]
        public string? Username { get; set; }

        [StringLength(60, MinimumLength = 6)]
        public string? PasswordHash { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string? Email { get; set; }

        [RegularExpression(@"^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$",
            ErrorMessage = "Телефон должен быть в формате +7(XXX)XXX-XX-XX")]
        public string? PhoneNumber { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string? FullName { get; set; }

        public int? RoleId { get; set; }

        public bool? IsActive { get; set; }
    }
}