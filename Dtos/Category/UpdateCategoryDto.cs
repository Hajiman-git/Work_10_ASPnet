using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.Category
{
    public class UpdateCategoryDto
    {
        [Required]
        [StringLength(40, MinimumLength = 3)]
        public string CategoryName { get; set; } = null!;

        [StringLength(200)]
        public string Description { get; set; } = null!;
    }
}