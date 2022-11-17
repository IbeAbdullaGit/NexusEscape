//Concrete Command
public class ToggleDoorCommand : ICommand
{
    Door _door;

    public ToggleDoorCommand(Door door)
    {
        _door = door;
    }
    public void Execute()
    {
        _door.ToggleDoor();
    }
}

