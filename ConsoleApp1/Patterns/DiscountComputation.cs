namespace ConsoleApp1.Patterns;
//public class Discount
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public int MyProperty { get; set; }
//}
//public interface IDiscountStrategy
//{
//    decimal ApplyDiscount(decimal totalAmount);
//}
public abstract class DiscountHandler
{
    protected DiscountHandler _nextHandler;

    public void SetNextHandler(DiscountHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public abstract decimal ApplyDiscount(decimal totalAmount, List<string> appliedDiscounts);
}
public class NoDiscountHandler : DiscountHandler
{
    public override decimal ApplyDiscount(decimal totalAmount, List<string> appliedDiscounts)
    {
        if (appliedDiscounts.Contains("No Discount"))
        {
            return totalAmount;
        }
        return _nextHandler?.ApplyDiscount(totalAmount, appliedDiscounts) ?? totalAmount;
    }
}
public class PercentageDiscountHandler : DiscountHandler
{
    private readonly decimal _discountPercentage;
    public PercentageDiscountHandler(decimal discountPercentage)
    {
        _discountPercentage = discountPercentage;
    }
    public override decimal ApplyDiscount(decimal totalAmount, List<string> appliedDiscounts)
    {
        if (appliedDiscounts.Contains("Percentage Discount"))
        {
            totalAmount = totalAmount - totalAmount * _discountPercentage / 100;
        }
        return _nextHandler?.ApplyDiscount(totalAmount, appliedDiscounts) ?? totalAmount;
    }
}
public class FixedAmountDiscountHandler : DiscountHandler
{
    private readonly decimal _discountAmount;
    public FixedAmountDiscountHandler(decimal discountAmount)
    {
        _discountAmount = discountAmount;
    }
    public override decimal ApplyDiscount(decimal totalAmount, List<string> appliedDiscounts)
    {
        if (appliedDiscounts.Contains("Fixed Amount Discount"))
        {
            totalAmount = totalAmount - _discountAmount;
        }

        return _nextHandler?.ApplyDiscount(totalAmount, appliedDiscounts) ?? totalAmount;
    }
}
public class LoyaltyDiscountHandler : DiscountHandler
{
    private readonly decimal _discountAmount;
    public LoyaltyDiscountHandler(decimal discountAmount)
    {
        _discountAmount = discountAmount;
    }
    public override decimal ApplyDiscount(decimal totalAmount, List<string> appliedDiscounts)
    {
        var aa = HandlerFactory.Create("Fixed Amount Discount");
        totalAmount = aa.ApplyDiscount(totalAmount, appliedDiscounts);
        return _nextHandler?.ApplyDiscount(totalAmount, appliedDiscounts) ?? totalAmount;
    }
}
public class HandlerFactory
{
    public static DiscountHandler Create(string type)
    {
        switch (type)
        {
            case "No Discount":
                return new NoDiscountHandler();
            case "Percentage Discount":
                return new PercentageDiscountHandler(10); // 10% discount
            case "Fixed Amount Discount":
                return new FixedAmountDiscountHandler(50); // $50 discount
            case "Loyalty":
                return new LoyaltyDiscountHandler(200); // $50 discount
            default:
                throw new ArgumentException("Invalid discount type");
        }
    }
}
public class DiscountComputation
{
    public static void Run()
    {
        decimal totalAmount = 1000;
        List<string> appliedDiscounts = new List<string> { "Percentage Discount", "Fixed Amount Discount" };
        // Create discount handlers
        var noDiscountHandler = new NoDiscountHandler();
        var percentageDiscountHandler = new PercentageDiscountHandler(10); // 10% discount
        var fixedAmountDiscountHandler = new FixedAmountDiscountHandler(50); // $50 discount
        var loyalty = new LoyaltyDiscountHandler(200); // $50 discount
        // Set up the chain of responsibility
        noDiscountHandler.SetNextHandler(percentageDiscountHandler);
        percentageDiscountHandler.SetNextHandler(fixedAmountDiscountHandler);
        //fixedAmountDiscountHandler.SetNextHandler(loyalty);
        // Apply discounts
        decimal finalAmount = noDiscountHandler.ApplyDiscount(totalAmount, appliedDiscounts);
        Console.WriteLine($"Final amount after applying discounts: {finalAmount}");
    }
}
