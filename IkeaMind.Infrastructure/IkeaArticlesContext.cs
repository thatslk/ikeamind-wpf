using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace IkeaMind.Infrastructure
{
    public partial class IkeaArticlesContext : DbContext
    {
        public IkeaArticlesContext()
        {
        }

        public IkeaArticlesContext(DbContextOptions<IkeaArticlesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<IkeaProduct> IkeaProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(ConfigurationManager.ConnectionStrings["IkeaDemoDB"].ConnectionString);
                optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
