namespace ConsoleApp1.Patterns;

public class Engine
{
    public string Name { get; set; }
}
public class Car
{
    public string Name { get; set; }
    public int Tire { get; set; }
    public int Door { get; set; }
    public Engine Engine { get; set; }
    public Car()
    {
        Engine = new Engine();
    }
    public override string ToString()
    {
        return $"Tire:{Tire} Door: {Door} Engine:{Engine.Name}";
    }
}
public class CarBuilder
{
    private Car car;
    public CarBuilder()
    {
        car = new Car();
    }

    public CarBuilder SetDoor(int door)
    {
        car.Door = door;
        return this;
    }
    public CarBuilder SetTire(int Tire)
    {
        car.Tire = Tire;
        return this;
    }
    public CarBuilder SetEngine(Engine engine)
    {
        car.Engine = engine;
        return this;
    }
    public Car Build()
    {
        return car;
    }
}
public class FE1 : Engine
{
    public FE1()
    {
        Name = "Fe1";
    }
}
public class FE2 : Engine
{
    public FE2()
    {
        Name = "Fe2";
    }
}