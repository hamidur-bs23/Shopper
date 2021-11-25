using DemoBS23.DAL.Entities.Auth;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoBS23.DAL.DatabaseContext
{
    class AuthDbContext : IdentityDbContext<AuthUser>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        public DbSet<AuthRefreshToken> AuthRefreshTokens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DateTime _modifiedDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            modelBuilder.Entity<AuthRefreshToken>().Property(refreshtoken => refreshtoken.CreationDate).IsRequired(true).HasDefaultValue(DateTime.UtcNow).HasColumnName("creation_date");
            modelBuilder.Entity<AuthRefreshToken>().Property(refreshtoken => refreshtoken.ModifiedDate).IsRequired(true).HasDefaultValue(_modifiedDate).HasColumnName("modified_date");

            base.OnModelCreating(modelBuilder);
        }
    }
}
