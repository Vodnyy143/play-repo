using Microsoft.EntityFrameworkCore;

namespace ShoesApp.Models;

public class AppDbContext: DbContext
{
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Unit> Units { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product>  Products { get; set; }
    public DbSet<Order>  Orders { get; set; }
    public DbSet<PickupPoint>  PickupPoints { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=admin;Password=admin;Database=wpf");
    }
}