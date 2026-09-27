using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagement.Models
{           
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = null!;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Range(0, double.MaxValue)]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public DateTime CreatedAt { get; set; }

        // concurrency token
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}