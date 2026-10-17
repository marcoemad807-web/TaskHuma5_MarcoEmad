using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductsCleanApi.Application.Interfaces;
using ProductsCleanApi.Domain.Entities;
using ProductsCleanApi.Infrastructure.Persistence;

namespace ProductsCleanApi.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(AppDbContext context, ILogger<ProductRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Products
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync(ct);
    }

    public async Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        return await _context.Products
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public async Task AddAsync(Product product, CancellationToken ct = default)
    {
        await _context.Products.AddAsync(product, ct);
        await _context.SaveChangesAsync(ct);

        _logger.LogInformation(
            "Product created. ProductId: {ProductId}, Name: {ProductName}",
            product.Id, product.Name);
    }
}