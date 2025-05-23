using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Patterns;

using ConsoleApp1.NewFolder;
using System;
using System.Collections.Generic;

// Memento class that stores the state
class Memento
{
    public Employee State { get; }

    public Memento(Employee state)
    {
        State = state;
    }
}
// Originator class that creates and restores states
class Originator
{
    private Employee _state;

    public void SetState(Employee state)
    {
        _state = state;
        Console.WriteLine($"State set to: {state}");
    }

    public Memento SaveState()
    {
        return new Memento(_state);
    }

    public void RestoreState(Memento memento)
    {
        _state = memento.State;
        Console.WriteLine($"State restored to: {_state}");
    }
}
// Caretaker class that manages mementos
class Caretaker
{
    private readonly Stack<Memento> _history = new Stack<Memento>();

    public void Save(Originator originator)
    {
        _history.Push(originator.SaveState());
    }

    public void Undo(Originator originator)
    {
        if (_history.Count > 0)
        {
            originator.RestoreState(_history.Pop());
        }
        else
        {
            Console.WriteLine("No saved states to restore.");
        }
    }
}
// Demo class to test the Memento Pattern
public class MementoClientApp
{
    public void run()
    {
        Originator originator = new Originator();
        Caretaker caretaker = new Caretaker();

        originator.SetState(new Employee { Id=1, Name="1" });
        caretaker.Save(originator);

        originator.SetState(new Employee { Id=2, Name="2" });
        caretaker.Save(originator);

        originator.SetState(new Employee { Id=3, Name="3" });
        originator.SetState(new Employee { Id=4, Name="4" });
        caretaker.Save(originator);

        // Undo operations
        caretaker.Undo(originator);
        caretaker.Undo(originator);
    }
}