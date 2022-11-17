//Client


using UnityEngine;

public class OpenDoorButton : MonoBehaviour
{
    public Door _door;
    DoorInvoker _doorInvoker;

    private void Start()
    {

        _doorInvoker = new DoorInvoker();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //ICommand openDoorCommand = new OpenDoorCommand(_door);
            //_doorInvoker.AddCommand(openDoorCommand);

            ICommand toggleDoorCommand = new ToggleDoorCommand(_door);
            _doorInvoker.AddCommand(toggleDoorCommand);
        }
    }
}
