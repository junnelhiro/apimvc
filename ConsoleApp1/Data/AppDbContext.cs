using ConsoleApp1.NewFolder;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>()
            .HasData(
            new Employee() { Id = 1, Name = "john" }
            );

        modelBuilder.Entity<TimeLog>()
         .HasData(
             new TimeLog() { Id = 1, EmployeeId = 1, LogTime = new DateTime(2025, 5, 1, 8, 0, 0), LogType = "TimeIn", WorkDate = new DateTime(2025, 5, 1) },
             new TimeLog() { Id = 2, EmployeeId = 1, LogTime = new DateTime(2025, 5, 1, 17, 0, 0), LogType = "Timeout", WorkDate = new DateTime(2025, 5, 1) }
         );
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<TimeLog> TimeLogs { get; set; }
    public DbSet<RoleAuthLabelModel>  RoleAuthLabels { get; set; }
    public DbSet<RoleModel>  Roles { get; set; }
    public DbSet<UserModel>  Users { get; set; }
    public DbSet<RoleUserModel>  RoleUsers { get; set; }
}
