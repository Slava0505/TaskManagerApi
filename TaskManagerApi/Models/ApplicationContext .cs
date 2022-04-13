using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

public class ApplicationContext : DbContext
{
    public DbSet<Topic> Topics { get; set; }
    public DbSet<User> Users { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Topic>()
            .HasOne(c => c.ParentTopic).WithMany(i => i.ChildTopics)
            .HasForeignKey(e => e.ParentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}