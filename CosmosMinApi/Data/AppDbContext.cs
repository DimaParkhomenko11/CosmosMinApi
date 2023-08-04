using CosmosMinApi.Domains;
using Microsoft.EntityFrameworkCore;

namespace CosmosMinApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }
    
    public DbSet<Product> Products => Set<Product>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .ToContainer("Products")
            .HasPartitionKey(p => p.Id);
        
        base.OnModelCreating(modelBuilder);
    }
}