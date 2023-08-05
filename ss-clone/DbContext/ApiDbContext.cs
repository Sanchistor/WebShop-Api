using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WebShop.WebShop.Data.Models;

namespace ss_clone.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Profile)
            .WithOne(p => p.User)
            .HasForeignKey<Profile>(p => p.UserId);
        modelBuilder.Entity<Profile>()
            .HasMany(e => e.Products)
            .WithOne(e => e.Profile)
            .HasForeignKey(e => e.ProfileId);
        modelBuilder.Entity<Section>()
            .HasMany(e => e.Categories)
            .WithOne(e => e.Section)
            .HasForeignKey(e => e.SectionId);
        modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(e => e.CategoryId);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Section)
            .WithMany(c => c.Products)
            .HasForeignKey(e => e.SectionId);
    }

    public virtual DbSet<User> Users { get; set; } = null!;
    public virtual DbSet<Profile> Profiles { get; set; } = null!;
    public virtual DbSet<Product> Products { get; set; } = null!;
    public virtual DbSet<Section> Sections { get; set; } = null!;
    public virtual DbSet<Category> Categories { get; set; } = null!;
    public virtual DbSet<Brand> Brands { get; set; } = null!;
}

