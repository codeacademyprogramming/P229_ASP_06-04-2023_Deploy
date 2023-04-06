using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P229FirstApi.Configurations;
using P229FirstApi.Entities;

namespace P229FirstApi.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());


            //Seed Data
            modelBuilder.Entity<Category>().HasData(
                new Category { CreatedAt = DateTime.UtcNow.AddHours(4), CreatedBy = "System",Id=1,Name="Apple",IsDeleted = false },
                new Category { CreatedAt = DateTime.UtcNow.AddHours(4), CreatedBy = "System", Id = 2, Name = "Asus", IsDeleted = false },
                new Category { CreatedAt = DateTime.UtcNow.AddHours(4), CreatedBy = "System", Id = 3, Name = "Lenovo", IsDeleted = false },
                new Category { CreatedAt = DateTime.UtcNow.AddHours(4), CreatedBy = "System", Id = 4, Name = "Dell", IsDeleted = false },
                new Category { CreatedAt = DateTime.UtcNow.AddHours(4), CreatedBy = "System", Id = 5, Name = "Acer", IsDeleted = false },
                new Category { CreatedAt = DateTime.UtcNow.AddHours(4), CreatedBy = "System", Id = 6, Name = "Samsung", IsDeleted = false }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1,CategoryId =1, CreatedBy = "System", CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 2,CategoryId =1, CreatedBy = "System", CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 3,CategoryId =1, CreatedBy = "System", CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 4,CategoryId =2, CreatedBy = "System", CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 5,CategoryId =2, CreatedBy = "System",  CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 6,CategoryId =3, CreatedBy = "System",  CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 7,CategoryId =3, CreatedBy = "System",  CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 8,CategoryId =3, CreatedBy = "System",  CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 9,CategoryId =3, CreatedBy = "System",  CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 10,CategoryId =4, CreatedBy = "System",  CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 11,CategoryId =5, CreatedBy = "System",  CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 12,CategoryId =5, CreatedBy = "System",  CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false },
                new Product { Id = 13,CategoryId =5, CreatedBy = "System",  CreatedAt = DateTime.UtcNow.AddHours(4), IsDeleted = false }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
