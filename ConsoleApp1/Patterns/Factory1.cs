namespace ConsoleApp1.Patterns;

public interface IProduct
{
}
public abstract class Product : IProduct
{
    public string Color { get; set; }
}
public interface IFinishing
{
    void DoActualFinishing(Product product);
}
public interface IFurniture
{
    IProduct CreateProduct(string type);
    IFinishing CreateFinishing(string type);
}

public class Chair : Product
{
}
public class Table : Product
{
}
public class Drawer : Product
{
}
public class Matted : IFinishing
{
    public void DoActualFinishing(Product product)
    {
        product.Color = "matted";
        Console.WriteLine(product.Color);
    }
}
public class Glow : IFinishing
{
    public void DoActualFinishing(Product product)
    {
        product.Color = "Glow";
        Console.WriteLine(product.Color);
    }
}
public class Soft : IFinishing
{
    public void DoActualFinishing(Product product)
    {
        product.Color = "soft";
        Console.WriteLine(product.Color);
    }
}
public class Hard : IFinishing
{
    public void DoActualFinishing(Product product)
    {
        product.Color = "hard";
        Console.WriteLine(product.Color);
    }
}
public class Factory1 : IFurniture
{
    public IFinishing CreateFinishing(string type)
    {
        type = type.ToLower();
        if (type == "soft")
        {
            return new Soft();
        }
        else if (type == "hard")
        {
            return new Hard();
        }
        else if (type == "glow")
        {
            return new Glow();
        }
        return new Matted();
    }

    public IProduct CreateProduct(string type)
    {
        type = type.ToLower();
        if (type == "chair")
        {
            return new Chair();
        }
        else if (type == "table")
        {
            return new Table();
        }
        return new Drawer();
    }
}

public class ClientFactory
{
    public ClientFactory()
    {
        Assemble(new Factory1());
    }
    public void Assemble(Factory1 factory)
    {
        IProduct product = factory.CreateProduct("chair");
        IFinishing finishing = factory.CreateFinishing("glow");
        finishing.DoActualFinishing((Product)product);
    }
}
