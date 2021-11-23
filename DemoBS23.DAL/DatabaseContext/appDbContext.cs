//using DemoBS23.DAL.Entities;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace DemoBS23.DAL.DatabaseContext
//{
//    public class appDbContext : DbContext
//    {
//        public class OptionsBuild
//        {
//            public OptionsBuild()
//            {
//                dbConfigSettings = new AppConfig();
//                optionsBuilder = new DbContextOptionsBuilder<appDbContext>();
//                optionsBuilder.UseSqlServer(dbConfigSettings.dbConnectionString);
//                dbOptions = optionsBuilder.Options;
//            }
//            private AppConfig dbConfigSettings { get; set; }
//            public DbContextOptionsBuilder<appDbContext> optionsBuilder { get; set; }
//            public DbContextOptions<appDbContext> dbOptions { get; set; }
            
//        }

//        public appDbContext(DbContextOptions<appDbContext> options) : base(options) 
//        {

//        }

//        #region DbSets  
//        public DbSet<Product> Products { get; set; }
//        public DbSet<Category> Categories { get; set; }

//        #endregion

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            //base.OnModelCreating(modelBuilder);
//            #region Product
//            modelBuilder.Entity<Product>().HasKey(p => p.Id);
//            modelBuilder.Entity<Product>().Property(p=>p.Id).UseIdentityColumn(1,1);
//            modelBuilder.Entity<Product>().Property(p=>p.Name).IsRequired(true).HasMaxLength(255);

//            //modelBuilder.Entity<Product>()
//            //    .HasMany<Category>(p => p.Categories)
//            //    .WithMany(c => c.Products);
//            #endregion

//            #region Category
//            modelBuilder.Entity<Category>().HasKey(c => c.Id);
//            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(255).IsRequired(true);
//            #endregion



//        }

//    }
//}
