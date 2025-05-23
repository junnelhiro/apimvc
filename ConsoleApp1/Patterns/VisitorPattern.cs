namespace ConsoleApp1.Patterns;
public interface IVistor
{
    void VisitC1(C1 component);
    void VisitC2(C2 component);
    void VisitC3(C3 component);
}


public class Deserializer : IVistor
{
    public void VisitC1(C1 component)
    {
        var orig = component.person.Name;
        var bty = Convert.FromBase64String(component.person.Name);
        var txt = Encoding.UTF8.GetString(bty);
        component.person.Name = txt;
        Console.WriteLine($"DeSerialize value [{orig}] :" + component.person.Name);
    }

    public void VisitC2(C2 component)
    {
        Console.WriteLine(component);
    }

    public void VisitC3(C3 component)
    {
        Console.WriteLine(component);
    }
}
public class Serializer : IVistor
{
    public void VisitC1(C1 component)
    {
        var orig = component.person.Name;
        var bytes = Encoding.UTF8.GetBytes(component.person.Name);
        component.person.Name = Convert.ToBase64String(bytes);
        Console.WriteLine($"Serialize value [{orig}] :" + component.person.Name);
    }

    public void VisitC2(C2 component)
    {
        Console.WriteLine(component);
    }

    public void VisitC3(C3 component)
    {
        Console.WriteLine(component);
    }
}
public interface IComponent
{
    void Accept(IVistor vistor);
}
public class C1 : IComponent
{
    public Person person;
    public C1()
    {
        person = new Person();
    }
    public void Accept(IVistor vistor)
    {
        vistor.VisitC1(this);
    }
}
public class C2 : IComponent
{
    public void Accept(IVistor vistor)
    {
        vistor.VisitC2(this);
    }

}
public class C3 : IComponent
{
    public void Accept(IVistor vistor)
    {
        vistor.VisitC3(this);
    }
}
public class VisitorDP
{
    public VisitorDP()
    {
        IVistor vistor1 = new Serializer();
        IVistor vistor2 = new Deserializer();
        var c1 = new C1()
        {
            person = new Person { Name = "input Data" }
        };
        var c = new List<IComponent>
        {
            c1,
            //new C2(),
            //new C3(),
        };
        foreach (var component in c)
        {
            component.Accept(vistor1);
            component.Accept(vistor2);
        }
    }
}