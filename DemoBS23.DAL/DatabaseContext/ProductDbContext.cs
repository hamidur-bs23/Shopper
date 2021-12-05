using DemoBS23.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.DAL.DatabaseContext
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        #region DbSet<>
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property<string>(e => e.Name)
                    .HasMaxLength(50)
                    .IsRequired(true);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property<string>(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property<int>(e => e.Price).IsRequired();
                entity.Property<int>(e => e.Quantity).IsRequired();
                entity.Property<string>(e => e.Description).HasMaxLength(200).IsRequired(false);

                entity.HasOne<Category>(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property<string>(e => e.Name).HasMaxLength(50).IsRequired();
                entity.Property<string>(e=>e.ContactNumber).HasMaxLength(50).IsRequired();
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property<DateTime>(e => e.DateCreated).HasDefaultValueSql("getdate()");
                entity.Property<int>(e => e.Total).IsRequired();

                entity.HasOne<Customer>(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerId)
                    .IsRequired(true)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OrderDetail>(entity=>{
                entity.HasKey(e => e.Id);
                entity.Property<int>(e => e.Quantity).IsRequired();
                entity.Property<int>(e => e.UnitPrice).IsRequired();
                entity.Property<int>(e => e.SubTotal).IsRequired();

                entity.HasOne<Order>(od => od.Order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne<Product>(od => od.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(od => od.ProductId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Restrict);
            });

            //modelBuilder.Entity<OrderDetail>(entity =>
            //{
            //    entity.HasOne<Order>(od=>od.Order)
            //        .WithMany(o=>o.OrderDetails)
            //        .HasForeignKey(od=>od.OrderId);

            //    entity.HasOne<Product>(od => od.Product)
            //        .WithMany(p => p.OrderDetails)
            //        .HasForeignKey(od => od.ProductId);
            //});
        }
    }
}
