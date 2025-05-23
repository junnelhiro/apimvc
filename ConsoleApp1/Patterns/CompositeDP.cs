// Component abstract class
public abstract class Component
{
    public string Name;
    public decimal Amount;
    protected Component(string name, decimal amount)
    {
        Name = name;
        Amount = amount;
    }
    public abstract void Display(int depth);
    public abstract IEnumerable<Component> GetItemAndChild();
    public virtual decimal GetTotal() { return Amount; }

}

// Leaf class
public class Leaf : Component
{
    public Leaf(string name, decimal amount) : base(name, amount) { }

    public override void Display(int depth)
    {
        Console.WriteLine(new string('-', depth) + Name + '-' + (Amount));
    }

    public override IEnumerable<Component> GetItemAndChild()
    {
        yield return this;
    }
}

// Composite class
public class Composite : Component
{
    private readonly List<Component> _children = new();

    public Composite(string name) : base(name, 0) { }

    public void Add(Component component)
    {
        _children.Add(component);
    }

    public void Remove(Component component)
    {
        _children.Remove(component);
    }

    public override void Display(int depth)
    {
        if (Name != "root")
        {
            Console.WriteLine(new string('-', depth) + Name + " Total " + GetTotal());
        }
        foreach (var child in _children)
        {
            child.Display(depth + 2);
        }
    }
    public override decimal GetTotal()
    {
        var total = 0m;
        foreach (var child in _children)
        {
            total += child.GetTotal();
        }
        return total;
    }

    public override IEnumerable<Component> GetItemAndChild()
    {
        yield return this;
        foreach (var child in _children)
        {
            foreach (var subChild in child.GetItemAndChild())
            {
                yield return subChild;
            }
        }
    }
}

// Main class
public class CompositeDP
{
    public void Run()
    {
        // Create leaf components
        Leaf leaf1 = new Leaf("Leaf 1", 100);
        Leaf leaf2 = new Leaf("Leaf 2", 200);
        Leaf leaf3 = new Leaf("Leaf 2", 1000);

        // Create composite component
        Composite root = new Composite("root");
        Composite composite = new Composite("Asset");
        composite.Add(leaf1);
        composite.Add(leaf2);
        composite.Add(leaf3);
        root.Add(composite);

        // Create another composite component
        Composite composite2 = new Composite("Liab");
        composite2.Add(new Leaf("Leaf 3", 100));
        composite2.Add(new Leaf("Leaf 4", 50));
        composite2.Add(new Leaf("Leaf 5", 150));
        root.Add(composite2);

        Composite composite3 = new Composite("Equity");
        composite3.Add(new Leaf("Leaf 6", 300));
        composite3.Add(new Leaf("Leaf 7", 100));
        composite3.Add(new Leaf("Leaf 8", 50));

        Composite composite3Sub = new Composite("Equity-Sub");
        composite3Sub.Add(new Leaf("Equity-Sub 1", 50));
        composite3Sub.Add(new Leaf("Equity-Sub 2", 100));
        composite3.Add(composite3Sub);

        Composite composite4Sub = new Composite("Equity--sub Sub");
        composite4Sub.Add(new Leaf("Equity--sub Sub 1", 300));
        composite4Sub.Add(new Leaf("Equity--sub Sub 2", 100));
        composite3Sub.Add(composite4Sub);

        root.Add(composite3);

        root.Display(0);

        Console.WriteLine($"Total Asset " + composite.GetTotal().ToString("#,##0.00"));
        var total = composite2.GetTotal() + composite3.GetTotal();
        Console.WriteLine($"Total Liab + Equity {total.ToString("#,##0.00")}");

        //foreach (var child in root.GetItemAndChild())
        //{
        //    if (child is Composite)
        //    {
        //       Console.WriteLine("<b> " + child.Name + " </b>");
        //    }
        //    else 
        //        Console.WriteLine(child.Name);
        //}
    }
}