﻿using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Models;

public class ApplicationContext : DbContext
{
    public DbSet<Topic> Topics { get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();   // создаем базу данных при первом обращении
    }
<<<<<<< HEAD
=======
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Topic>().HasData(
                new Topic { Id = 1, Name = "Tom" },
                new Topic { Id = 2, Name = "Bob" },
                new Topic { Id = 3, Name = "Sam" }
        );
    }
>>>>>>> f7f75e7fd6fe86c631a21dd4fee2b50f5b6bef12
}