using System.ComponentModel.DataAnnotations;

namespace ProductManagement.DTOs
{
    public class CreateProductDto
    {
        [Required, StringLength(200)]
        public string Name { get; set; } = null!;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }
    }
}