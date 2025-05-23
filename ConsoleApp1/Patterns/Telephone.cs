namespace ConsoleApp1.Patterns
{

    public interface IState
    {
        void Handle(Telephone telephone);
    }
    public class Drop : IState
    {
        public void Handle(Telephone telephone)
        {
            telephone.State = new Pick();
            Console.WriteLine("Drop");
        }
    }
    public class Pick : IState
    {
        public void Handle(Telephone telephone)
        {
            telephone.State = new Answer();
            Console.WriteLine("Pick");
        }
    }

    public class Answer : IState
    {
        public void Handle(Telephone telephone)
        {
            telephone.State = new Drop();
            Console.WriteLine("Answer");

        }
    }

    public class Telephone
    {
        internal IState State { get; set; }
        public Telephone()
        {
            State = new Drop();
        }
        public Telephone(IState state)
        {
            State = state;
        }
        public void SetState(IState state)
        {
            State = state;
        }
        public void Process()
        {
            State.Handle(this);
        }
    }
}
