using Microsoft.EntityFrameworkCore;
using SportPit.Data.EntityTypeConfigurations;
using SportPit.Extensions;
using SportPit.Models;
namespace SportPit.Data;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
    public required DbSet<Product> Products { get; set; }
    public required DbSet<Cart> Carts { get; set; }
    public required DbSet<User> Users { get; set; }
    public required DbSet<Category> Category { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyAllConfigurations();
        base.OnModelCreating(modelBuilder);
    }
}