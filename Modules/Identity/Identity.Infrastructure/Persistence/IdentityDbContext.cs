using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

using Identity = Domain.Models.Identity;
public class IdentityDbContext: DbContext
{
    public DbSet<Identity> Identities { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    public IdentityDbContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5433;Database=Planster2DB; Username=postgres; Password=Sekvoya55");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.Entity<Identity>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Identity>()
            .Property(p => p.Id)
            .ValueGeneratedNever();

        modelBuilder.Entity<Identity>()
            .OwnsOne(x => x.Credentials, credentials =>
            {
                credentials.Property(x => x.Password).HasColumnName("Password");
                credentials.Property(x => x.Email).HasColumnName("Email");
            });
        
        modelBuilder.Entity<RefreshToken>()
            .HasKey(p => p.IdentityId);

        modelBuilder.Entity<RefreshToken>()
            .HasOne(p => p.Identity)
            .WithOne(u => u.RefreshToken)
            .HasForeignKey<RefreshToken>(p => p.IdentityId);
    }
}