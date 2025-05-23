namespace ConsoleApp1.Patterns;

using System;

// Subsystem Classes
public class MigrationSystemA
{
    public void OperationA()
    {
        Console.WriteLine("SubsystemA: OperationA executed.");
    }
}

public class MigrationSystemB
{
    public void OperationB()
    {
        Console.WriteLine("SubsystemB: OperationB executed.");
    }
}

public class MigrationSystemC
{
    public void OperationC()
    {
        Console.WriteLine("SubsystemC: OperationC executed.");
    }
}

// Facade Class
public class MigrationFacade
{
    private readonly MigrationSystemA _subsystemA;
    private readonly MigrationSystemB _subsystemB;
    private readonly MigrationSystemC _subsystemC;

    public MigrationFacade()
    {
        _subsystemA = new MigrationSystemA();
        _subsystemB = new MigrationSystemB();
        _subsystemC = new MigrationSystemC();
    }

    public void Migrate()
    {
        Console.WriteLine("Facade: Coordinating subsystems...");
        _subsystemA.OperationA();
        _subsystemB.OperationB();
        _subsystemC.OperationC();
    }
}
