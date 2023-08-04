using CosmosMinApi.Data;
using CosmosMinApi.Domains;
using Microsoft.EntityFrameworkCore;

namespace CosmosMinApi.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Products
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _dbContext.Products.ToListAsync();
    }

    public async Task AddAsync(Product product)
    {
        await _dbContext.Products.AddAsync(product);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _dbContext.Entry(product).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var product = await _dbContext.Products
                          .FirstOrDefaultAsync(p => 
                              p.Id == id)
                      ?? throw new Exception("Not found");
        
        _dbContext.Products.Remove(product);
        await _dbContext.SaveChangesAsync();
        
    }
}