using Microsoft.EntityFrameworkCore;
using Users.Domain.Models;

namespace Users.Infrastructure.Persistence;

public class UsersDbContext: DbContext
{
    public DbSet<Domain.Models.User> Users { get; set; }
    public DbSet<ProfilePhoto> Photos { get; set; }
    
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.Entity<Domain.Models.User>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Domain.Models.User>()
            .HasMany(x => x.Contacts);
        
        modelBuilder.Entity<Domain.Models.User>()
            .HasMany(x => x.Photos)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Contact>()
            .HasKey(x => x.Id);
        
    }
}