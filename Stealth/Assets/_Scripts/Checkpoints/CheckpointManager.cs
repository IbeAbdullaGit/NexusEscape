using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
   private static CheckpointManager instance;
   public Vector3 lastCheckPointPos;

   public int checkpointNum = 0;

   [SerializeField] Door connectDoor;
   private void Awake() {
    
        if (instance ==null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
            Destroy(gameObject);
   }
   public void SetConnectedDoor(Door d)
   {
        connectDoor = d;
        DontDestroyOnLoad(connectDoor.gameObject);
        connectDoor.SetSpawnAgain();
   }
   public Door GetConnectedDoor()
   {
        return connectDoor;
   }
}
