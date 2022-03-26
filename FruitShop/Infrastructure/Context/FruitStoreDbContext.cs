using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models
{
    public class FruitStoreDbContext : DbContext
    {
        public FruitStoreDbContext(DbContextOptions<FruitStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Buy> Buy { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Fruit> Fruit { get; set; }
        public virtual DbSet<FruitType> FruitType { get; set; }
        public virtual DbSet<SellType> SellType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=FruitShop;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Buy>(entity =>
            {
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Buy)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Buy_Customer");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Fruit>(entity =>
            {
                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.FruitType)
                    .WithMany(p => p.Fruit)
                    .HasForeignKey(d => d.FruitTypeId)
                    .HasConstraintName("FK_Fruit_FruitType");
            });

            modelBuilder.Entity<FruitType>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SellType>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });
        }
    }
}
