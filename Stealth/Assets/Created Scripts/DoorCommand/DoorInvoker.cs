using System.Collections.Generic;

//Invoker

public class DoorInvoker
{
    Stack<ICommand> _commands;

    public DoorInvoker()
    {
        _commands = new Stack<ICommand>();
    }

    public void AddCommand(ICommand newCommand)
    {
        newCommand.Execute();
        _commands.Push(newCommand);
    }
}
