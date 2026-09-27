using System.ComponentModel.DataAnnotations;

namespace ProductManagement.DTOs
{
    public class UpdateProductDto
    {
        [Required]
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = null!;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        // client must supply RowVersion (base64 in JSON)
        [Required]
        public byte[] RowVersion { get; set; } = null!;
    }
}