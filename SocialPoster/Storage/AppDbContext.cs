using Microsoft.EntityFrameworkCore;

namespace SocialPoster.Storage;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InstagramAccount>().HasKey(x => x.Username);
        modelBuilder.Entity<QuoteEntity>().ToTable("Quotes");
        base.OnModelCreating(modelBuilder);
    }
}