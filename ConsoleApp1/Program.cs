using ConsoleApp1.Data;
using ConsoleApp1.NewFolder;
using Microsoft.Extensions.DependencyInjection;

IServiceCollection services = new ServiceCollection();
services.Config();
var provider = services.BuildServiceProvider();


var e = new Employee { Name = "added" };
var cnt = provider.GetService<AppDbContext>();
using (var context=cnt)
  {
    var trans=context.Database.BeginTransaction();
    context.Employees.Add(e);
    context.SaveChanges();
    trans.Commit();
}

Console.ReadKey();

public class OrderItems
{
    private readonly List<Item> _items;
    public OrderItems() { _items = new List<Item>(); }
    public void AddItem(Item item)
    {
        _items.Add(item);
    }
    public IReadOnlyList<Item> Items => _items;
}

public class  Item(string Name)
{
    public string Name { get; set; } = Name;
}
public record Publish(string Name)
{
    public Publish() : this("") { }
    public Publish AddInfo(string name) => this with { Name = name };
}