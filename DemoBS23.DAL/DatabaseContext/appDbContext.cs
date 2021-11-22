using DemoBS23.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.DAL.DatabaseContext
{
    public class appDbContext : DbContext
    {
        public class OptionsBuild
        {
            public OptionsBuild()
            {
                dbConfigSettings = new AppConfig();
                optionsBuilder = new DbContextOptionsBuilder<appDbContext>();
                optionsBuilder.UseSqlServer(dbConfigSettings.dbConnectionString);
                dbOptions = optionsBuilder.Options;
            }
            private AppConfig dbConfigSettings { get; set; }
            public DbContextOptionsBuilder<appDbContext> optionsBuilder { get; set; }
            public DbContextOptions<appDbContext> dbOptions { get; set; }
            
        }

        public appDbContext(DbContextOptions<appDbContext> options) : base(options) 
        {

        }

        #region DbSets  
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
