using Riptide;
using System.Collections.Generic;
using UnityEngine;

public class PlayerServer : MonoBehaviour
{
    public static Dictionary<ushort, PlayerServer> list = new Dictionary<ushort, PlayerServer>();

    public ushort Id { get; set; }
    public string Username { get; set; }

    private string username;

    private void OnDestroy()
    {
        list.Remove(Id);
    }

    public static void Spawn(ushort id, string username)
    {
        foreach (PlayerServer otherPlayer in list.Values)
            otherPlayer.SendSpawned(id);

        PlayerServer player = Instantiate(GameLogic.Singleton.Player2Prefab, GameLogic.Singleton.Player2Prefab.transform.position, Quaternion.identity).GetComponent<PlayerServer>();
        //ternary condition, if statement
        player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)})";
        player.Id = id;
        player.Username = string.IsNullOrEmpty(username) ? $"Guest {id}" : username;

        player.SendSpawned();
        list.Add(id, player);

        // PlayerServer player2;
        // //if (id == NetworkManagerClient.Singleton.Client.Id) //"client" is player 2
        // {
        //     player2 = Instantiate(GameLogicClient.Singleton.Player1Prefab, GameLogicClient.Singleton.Player1Prefab.transform.position, Quaternion.identity).GetComponent<PlayerServer>();
        //     //player2.IsLocal = true;
        // }
        // /* else
        // {
        //     player2 = Instantiate(GameLogicClient.Singleton.Player1Prefab, position, Quaternion.identity).GetComponent<PlayerClient>();
        //     player2.IsLocal = false;
        // } */

        // player2.name = $"Player hacker";
        // player2.Id = id;
        // player2.username = "hacker";

        // list.Add(id, player2);

        // Debug.Log("Player count: " + list.Count);
    }

    #region Messages
    public void SendSpawned()
    {
        NetworkManagerServer.Singleton.Server.SendToAll(AddSpawnData(Message.Create(MessageSendMode.Reliable, ServerToClientId.playerSpawned)));
    }

    private void SendSpawned(ushort toClientId)
    {
        NetworkManagerServer.Singleton.Server.Send(AddSpawnData(Message.Create(MessageSendMode.Reliable, ServerToClientId.playerSpawned)), toClientId);
    }

    private Message AddSpawnData(Message message)
    {
        message.AddUShort(Id);
        message.AddString(Username);
        message.AddVector3(transform.position);
        return message;
    }

    [MessageHandler((ushort)ClientToServerId.name)]
    private static void Name(ushort fromClientId, Message message)
    {
        //Spawn(fromClientId, message.GetString());

        //we don't need to spawn
    }

    [MessageHandler((ushort)ClientToServerId.input)]
    private static void Input(ushort fromClientId, Message message)
    {
        if (list.TryGetValue(fromClientId, out PlayerServer player))
            player.GetComponent<OtherPlayerMovement>().SetInput(message.GetBools(2), message.GetVector2(), message.GetVector2());
    }
    #endregion
}