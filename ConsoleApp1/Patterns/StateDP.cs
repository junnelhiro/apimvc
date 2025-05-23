namespace ConsoleApp1.Patterns;
public interface ITrafficLightState
{
    void Handle(TrafficLightContext context);
}
public class RedLightState : ITrafficLightState
{
    public void Handle(TrafficLightContext context)
    {
        Console.WriteLine("Red Light - Stop!");
        context.SetState(new GreenLightState());
    }
}

public class GreenLightState : ITrafficLightState
{
    public void Handle(TrafficLightContext context)
    {
        Console.WriteLine("Green Light - Go!");
        context.SetState(new YellowLightState());
    }
}

public class YellowLightState : ITrafficLightState
{
    public void Handle(TrafficLightContext context)
    {
        Console.WriteLine("Yellow Light - Slow Down!");
        context.SetState(new WarningLightState());
    }
}
public class WarningLightState : ITrafficLightState
{
    public void Handle(TrafficLightContext context)
    {
        Console.WriteLine("Warning Light  - Slow Down!");
        context.SetState(new RedLightState());
    }
}
public class TrafficLightContext
{
    private ITrafficLightState _state;
    public TrafficLightContext(ITrafficLightState state)
    {
        _state = state;
    }
    public void SetState(ITrafficLightState state)
    {
        _state = state;
    }
    public void Change()
    {
        _state.Handle(this);
    }
}
public class StateDPRunner
{
    public static void Run()
    {
        TrafficLightContext trafficLight = new TrafficLightContext(new RedLightState());
        for (int i = 0; i < 10; i++)
        {
            trafficLight.Change();
        }
    }
}