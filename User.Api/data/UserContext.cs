using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.model;

namespace User.Api.data
{
    public class UserContext:DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<AppUser>()
                .ToTable("Users")
                .HasKey(u => u.Id);

            modelBuilder.Entity<UserProperty>()
                .Property(u => u.Key).HasMaxLength(100);

            modelBuilder.Entity<UserProperty>()
                .Property(u => u.Value).HasMaxLength(100);

            modelBuilder.Entity<UserProperty>()
                .ToTable("UserProperties")
                .HasKey(u=>new {u.Key,u.AppUserId, u.Value });

            modelBuilder.Entity<UserTag>()
                .Property(u => u.Tag).HasMaxLength(100);

            modelBuilder.Entity<UserTag>()
                .ToTable("UserTags")
                .HasKey(u => new {  u.AppUserId, u.Tag});

            modelBuilder.Entity<BPFile>()
                .ToTable("BPFiles")
                .HasKey(u => u.Id);
        }
        public DbSet<AppUser> Users { get; set; }
    }
}
