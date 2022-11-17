
//Concrete Command
public class OpenDoorCommand : ICommand
{
    Door _door;

    public OpenDoorCommand(Door door)
    {
        _door = door;
    }
    public void Execute()
    {
        _door.OpenDoor();
    }
}
