public abstract class Command
{
    protected IReceiver _editor;
    protected Command(IReceiver editor)
    {
        _editor = editor;
    }
    public abstract void Execute();
    public abstract void Revert();
}
public class EditCommand : Command
{
    private string lastvalue;
    public EditCommand(IReceiver editor) : base(editor)
    {
    }
    public override void Execute()
    {
        lastvalue = _editor.Name;
        for (int i = 0; i < (lastvalue.Length + 1) * 2; i++)
        {
            _editor.Name = _editor.Name + "*";
        }
        _editor.Action(this);
    }

    public override void Revert()
    {
        try
        {
            lastvalue = _editor.Name.Substring(0, _editor.Name.Length - 1);
        }
        catch (Exception w)
        {
        }
        _editor.Name = lastvalue;
        _editor.Action(this);
    }
}
public class PasteCommand : Command
{
    public PasteCommand(IReceiver editor) : base(editor)
    {
    }
    public override void Execute()
    {
        _editor.Name = _editor.Name + "Paste";
        _editor.Counter += 1;
        _editor.Action(this);
    }
    public override void Revert()
    {
        _editor.Counter -= 1;
        _editor.Action(this);
    }
}
public interface IReceiver
{
    void Action(Command command);
    string Name { get; set; }
    int Counter { get; set; }
}
public class DocumentEditor : IReceiver
{
    public DocumentEditor()
    {
        Name = "";
        Counter = 0;
    }
    public string Name { get; set; }
    public int Counter { get; set; }

    public void Action(Command command)
    {
        Console.WriteLine(Name);
    }
}
public class Invoker
{
    Command command;
    public void SetCommand(Command command)
    {
        this.command = command;
    }
    public void ExecuteCommand()
    {
        command.Execute();
    }
    public void ExecuteRevert()
    {
        command.Revert();
    }
}
public class ClientDocumentEditor
{
    public ClientDocumentEditor()
    {
        IReceiver receiver = new DocumentEditor();
        Command command = new PasteCommand(receiver);
        Command editCommand = new EditCommand(receiver);
        Invoker invoker = new Invoker();
        invoker.SetCommand(editCommand);
        invoker.ExecuteCommand();
        invoker.ExecuteCommand();
        invoker.ExecuteCommand();
        invoker.ExecuteRevert();
        invoker.ExecuteRevert();
        Console.ReadKey();
    }
}
