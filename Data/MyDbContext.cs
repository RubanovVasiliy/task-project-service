using Microsoft.EntityFrameworkCore;
using task_project_service.Models;
using Task = task_project_service.Models.Task;

namespace task_project_service.Data;

public sealed class MyDbContext: DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Task> Tasks => Set<Task>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Project>()
            .Property(e => e.Status)
            .HasConversion(
                v => v.ToString(),
                v => (ProjectStatus)Enum.Parse(typeof(ProjectStatus), v));
        
        modelBuilder
            .Entity<Task>()
            .Property(e => e.Status)
            .HasConversion(
                v => v.ToString(),
                v => (Models.TaskStatus)Enum.Parse(typeof(Models.TaskStatus), v));
    }

}