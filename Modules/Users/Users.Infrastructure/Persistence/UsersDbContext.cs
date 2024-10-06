using Microsoft.EntityFrameworkCore;

namespace Users.Infrastructure.Persistence;

public class UsersDbContext: DbContext
{
    public DbSet<Domain.Models.User> Users { get; set; }
    
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }
    
    /*public UsersDbContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5433;Database=Planster2DB; Username=postgres; Password=Sekvoya55");
    }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.Entity<Domain.Models.User>()
            .HasKey(x => x.Id);
    }
}