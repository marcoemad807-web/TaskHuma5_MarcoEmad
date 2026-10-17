using ProductsCleanApi.Domain.Entities;

namespace ProductsCleanApi.Application.Interfaces;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync(CancellationToken ct = default);
    Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(Product product, CancellationToken ct = default);
}