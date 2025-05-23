using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Patterns;

public interface IInventoryListener
{
    void Listen(string itemName, int qty);
}
public class InventoryListenerComparer : IEqualityComparer<IInventoryListener>
{
    public bool Equals(IInventoryListener? x, IInventoryListener? y)
    {
        if (x == null || y == null) return false;
        return x?.GetType() == y?.GetType();
    }
    public int GetHashCode(IInventoryListener obj)
    {
        return obj.GetHashCode();
    }
}

public class InventoryListenerComparer2 : IComparer<IInventoryListener>
{
    public int Compare(IInventoryListener? x, IInventoryListener? y)
    { 
         return x?.GetType()== y?.GetType() ? 1: 0;
    }
}



public class Inventory
{
    public HashSet<IInventoryListener> listeners = new HashSet<IInventoryListener>(new InventoryListenerComparer());
    public string ItemName { get; set; }
    public int Qty { get; set; }
    public void AddListener(IInventoryListener listener)
    {
        if (!listeners.Contains(listener, new InventoryListenerComparer()))
        {
            listeners.Add(listener);
            Notify(listener);
        } 
    }
    public Inventory(string itemName, int qty)
    {
        ItemName = itemName;
        Qty = qty;
        listeners = new HashSet<IInventoryListener>();
    }
    public void SetInventory(string itemName, int qty)
    {
        //save inv
        ItemName = itemName;
        Qty = qty;
        Notify();
    }
    public void Notify(IInventoryListener listener)
    {
        listener.Listen(ItemName, Qty);
        Console.WriteLine("----------------");
    }

    public void Notify()
    {
        foreach (var listener in listeners)
        {
            listener.Listen(ItemName, Qty);
        }
        Console.WriteLine("----------------");
    }
}
public class StockIn : IInventoryListener
{
    public void Listen(string itemName, int qty)
    {
        Console.WriteLine($"{GetType().Name}: {itemName} {qty}");
    }
}
public class StockOut : IInventoryListener
{
    public void Listen(string itemName, int qty)
    {
        Console.WriteLine($"{GetType().Name}: {itemName} {qty}");
    }
}
public class PR : IInventoryListener
{
    public void Listen(string itemName, int qty)
    {
        Console.WriteLine($"{GetType().Name}: {itemName} {qty}");
    }
}

public class InventoryClientApp
{
    public InventoryClientApp()
    {
        var invhist = new Inventory("Default", 1);
        invhist.AddListener(new StockIn());
        invhist.AddListener(new StockOut());
        invhist.AddListener(new StockOut());
        invhist.AddListener(new StockOut());
        invhist.AddListener(new StockOut());
        invhist.AddListener(new StockOut());
        invhist.AddListener(new StockOut());
        invhist.AddListener(new StockOut());
        invhist.AddListener(new StockOut());
        invhist.AddListener(new StockOut());
        invhist.AddListener(new StockOut());
        invhist.SetInventory("item 2", 2);
        invhist.SetInventory("item 3", 5);
        invhist.AddListener(new PR());
        invhist.AddListener(new PR());
        invhist.SetInventory("item 10", 5);

    }
}
