using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

public class ApplicationContext : DbContext
{
    public DbSet<Topic> Topics { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
}