using Microsoft.EntityFrameworkCore;
using WorkOrganization.Domain.Models;
using TaskStatus = WorkOrganization.Domain.Models.TaskStatus;

namespace WorkOrganization.Infrastructure.Persistence;

public class WorkDbContext: DbContext
{
    public DbSet<ProjectTask> Tasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Sprint> Sprints { get; set; }
    public DbSet<Assignee> Assignees { get; set; }
    public DbSet<TaskStatus> TaskStatuses { get; set; }

    public WorkDbContext(DbContextOptions<WorkDbContext> options) : base(options) { }

    /*public WorkDbContext()
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=127.0.0.1;Port=5433;Database=Planster2DB; Username=postgres; Password=Sekvoya55");
    }*/
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("work");

        modelBuilder.Entity<ProjectTask>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<ProjectTask>()
            .HasOne(x => x.ParentTask)
            .WithMany(x => x.SubTasks)
            .HasForeignKey(x => x.ParentTaskId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<ProjectTask>()
            .Property(x => x.Code)
            .HasMaxLength(10);
        
        modelBuilder.Entity<ProjectTask>()
            .Property(x => x.Title)
            .HasMaxLength(200);

        modelBuilder.Entity<ProjectTask>()
            .HasOne(x => x.Project)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.ProjectId);

        modelBuilder.Entity<ProjectTask>()
            .HasOne(x => x.Status);
        
        modelBuilder.Entity<ProjectTask>()
            .HasOne(x => x.Assignee)
            .WithMany(x => x.Tasks);
        
        modelBuilder.Entity<ProjectTask>()
            .HasOne(x => x.Sprint)
            .WithMany(x => x.Tasks);

        modelBuilder.Entity<Project>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<Project>()
            .Property(p => p.Id)
            .ValueGeneratedNever();
        
        modelBuilder.Entity<Project>()
            .HasMany(x => x.Sprints)
            .WithOne(x => x.Project)
            .HasForeignKey(x => x.ProjectId);

        modelBuilder.Entity<Project>()
            .HasMany(x => x.Assignees)
            .WithMany(x => x.Projects);
        
        modelBuilder.Entity<Assignee>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<Sprint>()
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<TaskStatus>()
            .HasKey(x => x.Id);
    }
}