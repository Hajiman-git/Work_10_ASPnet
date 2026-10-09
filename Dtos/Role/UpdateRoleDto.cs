using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.Role
{
    public class UpdateRoleDto
    {
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string RoleName { get; set; } = null!;
    }
}