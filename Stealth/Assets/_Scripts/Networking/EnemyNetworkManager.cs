/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;

public class EnemyNetworkManager : MonoBehaviour
{
   public GameObject[] allEnemies;
   private static EnemyNetworkManager _singleton;

    public static EnemyNetworkManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(EnemyNetworkManager)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }
    public void Start()
    {
        //activate AI - perhaps do this more precise later
        allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        
    }
    private void Awake()
    {
        Singleton = this;
    }
    public void ActivateAIs()
    {
        for (int i=0; i< allEnemies.Length; i++)
        {
            //allEnemies[i].GetComponent<AINetworking>().StartEnemys();
        }
    }
    private void UpdateMovement(int id, Vector3 pos)
    {
        //find ai with matching id
        for (int i=0; i<allEnemies.Length; i++)
        {
           // if (allEnemies[i].GetComponent<AINetworking>().id == id)
            {
                //then set the AI
                //allEnemies[id].GetComponent<AINetworking>().SetPosition(pos);
                Debug.Log("Setting position " + pos);
                //stop loop now
                break;
            }
        }
    }
    #region Messages
    [MessageHandler((ushort)ServerToClientId.aiUpdate)]
    private static void MoveEnemy(Message message)
    {
        //get the number of the ai being sent
        int id = message.GetInt(); //should start at 0!
        //get their position
        var pos = message.GetVector3();
        EnemyNetworkManager.Singleton.UpdateMovement(id, pos);
        Debug.Log("Getting message");
        
    }
    #endregion
}
 */