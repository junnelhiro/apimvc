using ConsoleApp1.NewFolder;
using System.Linq.Expressions;
using System.Text.Json;

namespace ConsoleApp1.Patterns;

public class SingletonDP
{
    private SingletonDP() { }
    private static Lazy<Person> instance = new Lazy<Person>();
    public static Person Instance = instance.Value;
}


public interface IProto
{
    IProto Clone();
}

public class Proto : IProto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Person Person { get; set; }
    public Proto()
    {
        Person = new Person();
    }

    public IProto Clone()
    {
        // Serialize the object to JSON
        string json = JsonSerializer.Serialize(this);
        // Deserialize the JSON back to an object
        return JsonSerializer.Deserialize<Proto>(json);

    }
}



public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public int Zip { get; set; }
    public Country Country { get; set; }
}

public class Country
{
    public string Name { get; set; }
    public string Code { get; set; }
}

public static class ObjectMapper
{

    public static Func<TSource, TDestination> CreateMap<TSource, TDestination>()
    where TDestination : new()
    {
        var sourceParameter = Expression.Parameter(typeof(TSource), "source");
        var bindings = new List<MemberBinding>();

        foreach (var destProp in typeof(TDestination).GetProperties())
        {
            var sourceProp = typeof(TSource).GetProperty(destProp.Name);
            if (sourceProp != null && destProp.CanWrite)
            {
                var sourceValue = Expression.Property(sourceParameter, sourceProp);
                var binding = Expression.Bind(destProp, sourceValue);
                bindings.Add(binding);
            }
        }

        var body = Expression.MemberInit(Expression.New(typeof(TDestination)), bindings);
        var lambda = Expression.Lambda<Func<TSource, TDestination>>(body, sourceParameter);

        return lambda.Compile();
    }

    private static bool IsComplexType(Type type)
    {
        return type.IsClass && type != typeof(string);
    }

    class Singleton
    {
        static void Main(string[] args)
        {
            var employee = new Employee
            {
                Id = 1,
                Name = "John Doe",
            };

            var mapper = CreateMap<Employee, Person>();
            var person = mapper(employee);

            Console.WriteLine($"Person Id: {person.Id}, Name: {person.Name}, Address: {person.Address.Street}, {person.Address.City}, {person.Address.Country.Name}, {person.Address.Country.Code}");
        }
    }
}