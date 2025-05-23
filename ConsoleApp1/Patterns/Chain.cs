
abstract class Handler
{
    protected Handler _successor;

    public void SetSuccessor(Handler successor)
    {
        _successor = successor;
    }

    public abstract void HandleRequest(string request);
}

class ConcreteHandlerA : Handler
{
    public override void HandleRequest(string request)
    {
        if (request == "A")
        {
            Console.WriteLine("Handler A processed the request");
        }
        else if (_successor != null)
        {
            _successor.HandleRequest(request);
        }
    }
}

class ConcreteHandlerB : Handler
{
    public override void HandleRequest(string request)
    {
        if (request == "B")
        {
            Console.WriteLine("Handler B processed the request");
        }
        else if (_successor != null)
        {
            _successor.HandleRequest(request);
        }
    }
}

class Chain
{
    public Chain()
    {
        var handlerA = new ConcreteHandlerA();
        var handlerB = new ConcreteHandlerB();

        // Set up chain B -> A
        handlerB.SetSuccessor(handlerA);
        // Process requests
        handlerB.HandleRequest("A"); // Handled by Handler A
        handlerB.HandleRequest("B"); // Handled by Handler B
        handlerB.HandleRequest("B"); // Handled by Handler B
        handlerB.HandleRequest("A"); // Handled by Handler A
        handlerB.HandleRequest("A"); // Handled by Handler A
        handlerB.HandleRequest("A"); // Handled by Handler A
        handlerB.HandleRequest("A"); // Handled by Handler A
        handlerB.HandleRequest("A"); // Handled by Handler A
        handlerB.HandleRequest("B"); // Handled by Handler B
        handlerB.HandleRequest("B"); // Handled by Handler B
        handlerB.HandleRequest("B"); // Handled by Handler B
        handlerB.HandleRequest("C"); // Not handled
    }
}