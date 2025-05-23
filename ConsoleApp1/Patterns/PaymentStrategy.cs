using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Patterns
{
    public interface IPayment
    {
        void Pay(decimal amount);
    }
    public class CardInfo
    {
        public string Bank { get; set; }
    }
    public class CC : IPayment
    {
        private readonly CardInfo info;

        public CC(CardInfo info)
        {
             
            this.info = info;
        }

        public void Pay(decimal amount)
        {

            //
            Console.WriteLine("Card");
        }
    }
    public class Cash : IPayment
    {
        public void Pay(decimal amount)
        {
            Console.WriteLine("cash");

        }
    }

    public class PaymentStrategy
    {
        public PaymentStrategy()
        {
            IPayment strat = PaymentFactory.Create("CC");
            var context = new PaymentStratContext();
            context.SetStrat(strat);
            context.Apply(100);
        }
    }

    public class PaymentStratContext
    {
        private IPayment payment;
        public PaymentStratContext() { }
        public void SetStrat(IPayment payment) => this.payment = payment;
        public void Apply(decimal amount) => payment.Pay(amount);
    }
    public class PaymentFactory
    {
        public static IPayment Create(string type)
        {
            switch (type)
            {
                case "CC":
                    return new CC(new CardInfo());
                case "Cash":
                    return new Cash();
                default:
                    return new Cash();
            }
        }
    }
}


