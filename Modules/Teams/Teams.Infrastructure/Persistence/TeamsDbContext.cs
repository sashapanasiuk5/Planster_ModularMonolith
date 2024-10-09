using Microsoft.EntityFrameworkCore;
using Teams.Domain.Models;

namespace Teams.Infrastructure.Persistence;

public class TeamsDbContext: DbContext
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<Member> Members { get; set; }

    public TeamsDbContext(DbContextOptions<TeamsDbContext> options) : base(options)
    {
    }

    public TeamsDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=127.0.0.1;Port=5433;Database=Planster2DB; Username=postgres; Password=Sekvoya55");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("teams");
        modelBuilder.Entity<Project>()
            .HasKey(p => p.Id);
        
        modelBuilder.Entity<ProjectMember>()
            .HasKey(x => new { x.ProjectId, x.MemberId });
        modelBuilder.Entity<Project>()
            .HasMany(p => p.Members)
            .WithOne(m => m.Project)
            .HasForeignKey(p => p.ProjectId);
        
        modelBuilder.Entity<Member>()
            .HasMany(p => p.ProjectMembers)
            .WithOne(m => m.Member)
            .HasForeignKey(p => p.MemberId);
        
        modelBuilder.Entity<Member>()
            .HasKey(p => p.Id);
        modelBuilder.Entity<Member>()
            .Property(p => p.Id)
            .ValueGeneratedNever();
    }
}