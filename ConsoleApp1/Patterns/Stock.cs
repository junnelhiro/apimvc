namespace ConsoleApp1.Patterns;

public interface IInvestor
{
    void Update(Stock stock);
}
public abstract class Stock
{
    private string symbol;
    private double price;
    private List<IInvestor> investors = new List<IInvestor>();
    // Constructor
    public Stock(string symbol, double price)
    {
        this.symbol = symbol;
        this.price = price;
    }
    public void Attach(IInvestor investor)
    {
        investors.Add(investor);
        Notify1(investor);

    }
    public void Detach(IInvestor investor)
    {
        investors.Remove(investor);
    }
     public void Notify1(IInvestor investor)
    {
        investor.Update(this);
    }
    public void Notify()
    {
        foreach (IInvestor investor in investors)
        {
            investor.Update(this);
        }
        Console.WriteLine("");
    }
    // Gets or sets the price
    public double Price
    {
        get { return price; }
        set
        {
            if (price != value)
            {
                price = value;
                Notify();
            }
        }
    }
    // Gets the symbol
    public string Symbol
    {
        get { return symbol; }
    }
}
public class IBM : Stock
{
    // Constructor
    public IBM(string symbol, double price)
        : base(symbol, price)
    {
    }
}
public class MAC : Stock
{
    // Constructor
    public MAC(string symbol, double price)
        : base(symbol, price)
    {
    }
}
public class Investor : IInvestor
{
    private string name;
    private Stock stock;
    // Constructor
    public Investor(string name)
    {
        this.name = name;
    }
    public void Update(Stock stock)
    {
        this.stock = stock;
        Console.WriteLine("Notified {0} of {1}'s " +
            "change to {2:C}", name, stock.Symbol, stock.Price);
    }
    // Gets or sets the stock
    public Stock Stock
    {
        get { return stock; }
        set { stock = value; }
    }
}

public class StockClientApp
{
    public StockClientApp()
    {
        Stock Mac= new MAC("Mac",3);
        Mac.Attach(new Investor("Investor 1"));
        //Mac.Price = 5;
        //Mac.Price = 3;
    }
}