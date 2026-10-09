using System.ComponentModel.DataAnnotations;

namespace work_10.Dtos.Category
{
    public class PatchCategoryDto
    {
        [StringLength(40, MinimumLength = 3)]
        public string? CategoryName { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }
    }
}