using work_10.Dtos.Role;

namespace work_10.Dtos.Customer
{
    public class CustomerDto
    {
        public int IdCustomer { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public int RoleId { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public bool IsActive { get; set; }

        public RoleDto? Role { get; set; }
    }
}