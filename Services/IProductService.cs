using ProductManagement.DTOs;

namespace ProductManagement.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<ProductDto> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateProductDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}