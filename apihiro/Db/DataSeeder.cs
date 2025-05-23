using apihiro.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.CompilerServices;

namespace apihiro.Db;

public static class DataSeeder
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>()
            .HasData(
            new Department() { Id = 1, Description = "HR" }
            );

        modelBuilder.Entity<User>()
            .HasData(
                new User { Id = 1, Name = "jhon", Password = BCrypt.Net.BCrypt.HashPassword("code") }
            ); 
    }
}

