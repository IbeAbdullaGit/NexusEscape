using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Riptide;

public class EnemyID : MonoBehaviour
{
    public int id;
    private void Update() {
        
        SendPosition();
    }
    #region Messages
    private void SendPosition()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.aiUpdate);

        //send id first
        message.AddInt(id);
        //then send position
        message.AddVector3(transform.position);
        //send message
        NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
    #endregion
}
