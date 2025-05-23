namespace ConsoleApp1.Patterns;

public abstract class Logger
{
    protected Logger nextLogger;

    public void SetNext(Logger next)
    {
        nextLogger = next;
    }

    public void LogMessage(string message, int level)
    {
        if (HandleRequest(level))
        {
            ProcessMessage(message);
        }
        else if (nextLogger != null)
        {
            nextLogger.LogMessage(message, level);
        }
    }

    protected abstract bool HandleRequest(int level);
    protected abstract void ProcessMessage(string message);
}

public class ConsoleLogger : Logger
{
    protected override bool HandleRequest(int level) => level <= 1;

    protected override void ProcessMessage(string message)
    {
        Console.WriteLine($"Console Logger: {message}");
    }
}

public class FileLogger : Logger
{
    protected override bool HandleRequest(int level) => level == 2;

    protected override void ProcessMessage(string message)
    {
        Console.WriteLine($"File Logger: {message} (written to file)");
    }
}

public class DatabaseLogger : Logger
{
    protected override bool HandleRequest(int level) => level >= 3;

    protected override void ProcessMessage(string message)
    {
        Console.WriteLine($"Database Logger: {message} (stored in database)");
    }
}

public class ChainV1
{
    public ChainV1()
    {

        Logger handler1 = new FileLogger();
        Logger handler2 = new ConsoleLogger();
        Logger handler3 = new DatabaseLogger();

        handler1.SetNext(handler2);
        handler2.SetNext(handler3);

        handler1.LogMessage("hanfler", 1);
        handler1.LogMessage("hanfler", 2);
        handler1.LogMessage("hanfler", 3);
        handler1.LogMessage("hanfler", 4);
        handler1.LogMessage("hanfler", 1);
    }
}

