using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class IdentityDbContext: DbContext
{
    public DbSet<Domain.Models.Identity> Identities { get; set; }
    
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }

    /*public IdentityDbContext()
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5433;Database=Planster2DB; Username=postgres; Password=Sekvoya55");
    }*/

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("identity");
        modelBuilder.Entity<Domain.Models.Identity>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<Domain.Models.Identity>()
            .OwnsOne(x => x.Credentials, credentials =>
            {
                credentials.Property(x => x.Password).HasColumnName("Password");
                credentials.Property(x => x.Email).HasColumnName("Email");
            });
    }
}