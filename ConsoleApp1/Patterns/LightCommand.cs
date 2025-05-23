namespace ConsoleApp1.Patterns
{

    public interface ICommand
    {
        void Execute();
    }
    public interface ICommandReceiver
    {
        void Execute(bool value);
    }
    public class TurnOn : ICommand
    {
        private readonly ICommandReceiver receiver;

        public TurnOn(ICommandReceiver receiver)
        {
            this.receiver = receiver;
        }
        public void Execute()
        {
            receiver.Execute(true);
        }
    }
    public class TurnOff : ICommand
    {
        private readonly ICommandReceiver receiver;

        public TurnOff(ICommandReceiver receiver)
        {
            this.receiver = receiver;
        }
        public void Execute()
        {
            receiver.Execute(false);
        }
    }
    public class Blinking : ICommand
    {
        private readonly ICommandReceiver receiver;

        public Blinking(ICommandReceiver receiver)
        {
            this.receiver = receiver;
        }
        public void Execute()
        {
            var rnd = new Random(1);

            receiver.Execute(rnd.Next(1) == 0);
        }
    }
    public class Light : ICommandReceiver
    {
        private bool _isOn;
        public void Execute(bool value)
        {
            _isOn = value;
            Console.WriteLine(_isOn ? "Turn On" : "Turn Off");
        }
    }

    public class LightInvoker
    {
        private ICommand command;
        public LightInvoker()
        {
        }
        public void SetCommand(ICommand command)
        {
            this.command = command;
        }
        public void Execute()
        {
            command.Execute();
        }

    }


    public class LightCommand
    {
        public LightCommand()
        {
            var receiver = new Light();
            var command = new Blinking(receiver);
            var invoker = new LightInvoker();
            invoker.SetCommand(command);
            invoker.Execute();
        }
    }
}
