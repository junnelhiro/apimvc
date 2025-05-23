using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Patterns
{

    public interface IDiscount
    {
        decimal Apply(decimal amount);
    }
    public class NoDiscount : IDiscount
    {
        public decimal Apply(decimal amount)
        {
             return amount;
        }
    }
    public class Percentage : IDiscount
    {
        private decimal _percentage;
        public Percentage(decimal percentage)
        {
            _percentage = percentage;
        }
        public decimal Apply(decimal amount)
        {
             return amount - _percentage * amount;
        }
    }
    public class Fix : IDiscount
    {
        private decimal _discount;
        public Fix( decimal  discount)
        {
            _discount = discount;
        }
        public decimal Apply(decimal amount)
        {
            return amount - _discount;
        }
    }
    public class DiscountService
    {
        public DiscountService()
        {
          IDiscount discount = new Percentage(0.20m);
          Console.WriteLine(discount.Apply(300));
        }
    }
}
