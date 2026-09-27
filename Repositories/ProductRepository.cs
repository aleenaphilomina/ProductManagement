using Microsoft.EntityFrameworkCore;
using ProductManagement.Data;
using ProductManagement.Models;

namespace ProductManagement.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) => _db = db;

        public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Products.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Product?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
        {
            var entry = _db.Attach(product);
            entry.State = EntityState.Modified;
            if (product.RowVersion != null)
            {
                entry.Property(nameof(product.RowVersion)).OriginalValue = product.RowVersion;
            }
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}