using Riptide;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClient : MonoBehaviour
{
    public static Dictionary<ushort, PlayerClient> list = new Dictionary<ushort, PlayerClient>();

    public ushort Id { get; private set; }
    public bool IsLocal { get; private set; }

    private string username;
   //public C_PlayerPrediction clientprediction;
    private void OnDestroy()
    {
        list.Remove(Id);
    }

    public static void Spawn(ushort id, string username, Vector3 position)
    {
        PlayerClient player;
        
       
        {
            Debug.Log("Did we get here?");
            //THIS PART IS STILL IMPORTANT
            player = Instantiate(GameLogicClient.Singleton.Player1Prefab, position, Quaternion.identity).GetComponent<PlayerClient>();
            player.IsLocal = false;

            player.name = $"Player {id} (username)";
            player.Id = id;
            player.username = username;

            list.Add(id, player);
            Debug.Log("Spawning in player");
        }

       
    }
    #region Messages    
    [MessageHandler((ushort)ServerToClientId.playerSpawned)]
    private static void SpawnPlayer(Message message)
    {
        Debug.Log("Got message");
        Spawn(message.GetUShort(), message.GetString(), message.GetVector3());
        Debug.Log("Receiving spawn");
    }
    [MessageHandler((ushort)ServerToClientId.input)]
    private static void Input(Message message) //client side DOES NOT HAVE USHORT
    {
        //get the other player
        GameObject.FindGameObjectWithTag("Player").GetComponent<OtherPlayerServer>().SetInput(message.GetBools(2), message.GetVector2(), message.GetVector2());
        Debug.Log("Receiving movement");
    }
    [MessageHandler((ushort)ServerToClientId.inputSimple)]
    private static void InputSimple(Message message) //client side DOES NOT HAVE USHORT
    {
        //get the other player
        if (GameObject.FindGameObjectWithTag("Player"))
            GameObject.FindGameObjectWithTag("Player").GetComponent<OtherPlayerServer>().Move(message.GetVector3());
       // Debug.Log("Receiving move message");

    }
    /*   //handle cameras
    [MessageHandler((ushort)ServerToClientId.cameras)]
    private static void GetCameras(Message message) //client side DOES NOT HAVE USHORT
    {
        int num = message.GetInt();
        
        GameObject.FindGameObjectWithTag("NetworkClient").GetComponent<CamerasNetworking>().SetInitial(num);
       
        for (int i=0; i< num; i++) //cams is how many messages we sent
        {
            
            //unwrap the messages
            var cams = message.GetBytes();
            Texture2D tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);
            tex.LoadRawTextureData(cams);
            tex.Apply();
            //apply it to the object
            GameObject.FindGameObjectWithTag("NetworkClient").GetComponent<CamerasNetworking>().SetTexture(i, tex);
        }

        //for now apply to object?   
        Debug.Log("Got message!");
    }  */
    
    
     #endregion
  
}

