using Microsoft.EntityFrameworkCore;
using ProductManagement.DTOs;
using ProductManagement.Models;
using ProductManagement.Repositories;

namespace ProductManagement.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        public ProductService(IProductRepository repo) => _repo = repo;

        public async Task<ProductDto> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;
            var entity = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock,
                CreatedAt = now
            };

            await _repo.AddAsync(entity, cancellationToken);
            return new ProductDto(entity.Id, entity.Name, entity.Description, entity.Price, entity.Stock, entity.CreatedAt, entity.RowVersion);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _repo.GetByIdAsync(id, cancellationToken);
            if (existing == null) throw new KeyNotFoundException("Product not found");
            await _repo.DeleteAsync(existing, cancellationToken);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var items = await _repo.GetAllAsync(cancellationToken);
            return items.Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CreatedAt, p.RowVersion));
        }

        public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var p = await _repo.GetByIdAsync(id, cancellationToken);
            if (p == null) return null;
            return new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.CreatedAt, p.RowVersion);
        }

        public async Task UpdateAsync(UpdateProductDto dto, CancellationToken cancellationToken = default)
        {
            var existing = await _repo.GetByIdAsync(dto.Id, cancellationToken);
            if (existing == null) throw new KeyNotFoundException("Product not found");

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Stock = dto.Stock;
            existing.RowVersion = dto.RowVersion;

            try
            {
                await _repo.UpdateAsync(existing, cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new InvalidOperationException("Concurrency conflict - the product was updated by someone else.");
            }
        }
    }
}