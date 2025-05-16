using apihiro.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace apihiro.Db;

public class WebContext : DbContext
{
    public WebContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Person> People { get; set; }
}
