using EfCoreProjectInternship.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace EfCoreProjectInternship.Data
{
    public class BloggingDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("")
                .LogTo(Console.WriteLine, LogLevel.Information)
                //caused issues with adding blogs
                //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                //Do NOT use thes following 2 in a production workload
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            ;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<Post>().Where(q => q.State == EntityState.Modified || q.State == EntityState.Added);

            foreach (var entry in entries) 
            {
                entry.Entity.ModifiedDate= DateTime.UtcNow;
                
                if(entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate= DateTime.UtcNow;
                }

            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
