using Microsoft.EntityFrameworkCore;
using Shoporium.Data._EntityFramework.Entities;

namespace Shoporium.Data._EntityFramework
{
    public partial class ShoporiumContext : DbContext
    {
        public ShoporiumContext()
        {
        }

        public ShoporiumContext(DbContextOptions<ShoporiumContext> options)
            : base(options)
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<LoginDetail> LoginDetails { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { Id = 1, Name = "Електроніка" },
                new ProductCategory { Id = 2, Name = "Хобі та спорт" },
                new ProductCategory { Id = 3, Name = "Одяг, взуття та аксесуари" },
                new ProductCategory { Id = 4, Name = "Дитячі товари" },
                new ProductCategory { Id = 5, Name = "Краса та здоров'я" },
                new ProductCategory { Id = 6, Name = "Дім і сад" },
                new ProductCategory { Id = 7, Name = "Транспорт" },
                new ProductCategory { Id = 8, Name = "Будівництво та ремонт" },
                new ProductCategory { Id = 9, Name = "Обладнання та сировина" },
                new ProductCategory { Id = 10, Name = "Тварини і рослини" },
                new ProductCategory { Id = 11, Name = "Нерухомість" },
                new ProductCategory { Id = 12, Name = "Робота" },
                new ProductCategory { Id = 13, Name = "Послуги та бізнес" },
                new ProductCategory { Id = 14, Name = "Продукти харчування" }
            );
        }
    }
}
