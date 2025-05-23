using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using static ConsoleApp1.Data.ServicesRegistration;
namespace ConsoleApp1.Data;




//public class TestDbContextFactory<T> : IDbContextFactory<T> where T : DbContext
//{
//    private readonly DbContextOptions<T> _options;
//    public TestDbContextFactory(DbContextOptions<T> options)
//    {
//        _options = options;
//    }
//    public T CreateDbContext()
//    {
//        return (T)Activator.CreateInstance(typeof(T), _options)!;
//    }
//}

//public class MyTest : TestDbContextFactory<AppDbContext>
//{
//    protected MyTest() : base(CreateOptions())
//    {
//        var context = CreateDbContext();
//    }
//    private static DbContextOptions<AppDbContext> CreateOptions()
//    {
//        return new DbContextOptionsBuilder<AppDbContext>()
//        .UseSqlServer("test")
//        .Options;
//    }
//}
