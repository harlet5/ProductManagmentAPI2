using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using ProductManagmentAPI.Models;

namespace ProductManagmentAPI.Data;

public class ApiDB : DbContext
{
    public ApiDB(DbContextOptions<ApiDB> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductGroup> ProductGroups { get; set; }
    public DbSet<Store> Stores { get; set; }
    public DbSet<ProductStore> ProductStores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductGroup>()
            .HasMany(pg => pg.ChildGroups)
            .WithOne(pg => pg.ParentGroup)
            .HasForeignKey(pg => pg.ParentGroupId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.ProductGroup)
            .WithMany(pg => pg.Products)
            .HasForeignKey(p => p.ProductGroupId);

        modelBuilder.Entity<ProductStore>()
            .ToTable("ProductStores")
            .HasKey(ps => new { ps.ProductId, ps.StoreId });

        modelBuilder.Entity<ProductStore>()
            .HasOne(ps => ps.Product)
            .WithMany(p => p.ProductStores)
            .HasForeignKey(ps => ps.ProductId);

        modelBuilder.Entity<ProductStore>()
            .HasOne(ps => ps.Store)
            .WithMany(s => s.ProductStores)
            .HasForeignKey(ps => ps.StoreId);

        modelBuilder.Entity<Store>().HasData(
            new Store { Id = 1, Name = "Coop" },
            new Store { Id = 2, Name = "Selver" },
            new Store { Id = 3, Name = "Maxima" }
        );

        modelBuilder.Entity<ProductGroup>().HasData(
            new ProductGroup { Id = 1, Name = "Toidud", ParentGroupId = null },
            new ProductGroup { Id = 2, Name = "Puuviljad", ParentGroupId = 1 },
            new ProductGroup { Id = 3, Name = "Lihatooted", ParentGroupId = 1 },
            new ProductGroup { Id = 4, Name = "Joogid", ParentGroupId = null },
            new ProductGroup { Id = 5, Name = "Karastusjoogid", ParentGroupId = 4 },
            new ProductGroup { Id = 6, Name = "Vesi", ParentGroupId = 4 }
        );
    }
}
