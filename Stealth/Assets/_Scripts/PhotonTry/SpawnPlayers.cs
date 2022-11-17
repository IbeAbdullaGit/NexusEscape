using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public GameObject playerPrefab2;


    public Transform[] spawnPoints;
    public Button startGameButton;
    public TMP_Text statusText;

   public GameObject lobbyCam;
    public GameObject lobbyUI;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        
        startGameButton.onClick.AddListener(StartGame);
        startGameButton.interactable = false;
        
    }
    private void Update() {
        //statusText.text = PhotonNetwork.connectionStateDetailed.ToString();
        
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
        statusText.text = PhotonNetwork.PlayerList.Length.ToString() + " Player(s) Connected.";
    }
    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new Photon.Realtime.RoomOptions(){IsVisible = false, MaxPlayers = 2}, Photon.Realtime.TypedLobby.Default);
    }
    public override void OnJoinedRoom()
    {
        statusText.text = PhotonNetwork.PlayerList.Length.ToString() + " Player(s) Connected.";
        if (PhotonNetwork.IsMasterClient)
        {
            startGameButton.interactable = true;
        }
    }
    void StartGame()
    {
        //PhotonNetwork.Instantiate(playerPrefab.name, player1Pos.position, Quaternion.identity);
        var players = PhotonNetwork.PlayerList;
         for (int i=0; i < players.Length; i++)
        {
            photonView.RPC("RPCStartGame", players[i], spawnPoints[i].position, i);
        } 


        /* //maybe set this for specific players
        //1st player
        if (players.Length == 1)
        {
            //photonView.RPC("RPCStartGame", players[0], spawnPoints[0].position);
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPoints[0].position, Quaternion.identity);
        }
        else if (players.Length ==2) //2nd player
        {
            //photonView.RPC("RPCStartGame", players[1], spawnPoints[1].position);
            PhotonNetwork.Instantiate(playerPrefab2.name, spawnPoints[1].position, Quaternion.identity);
        } */
    }
    [PunRPC]
    void RPCStartGame(Vector3 spawnPos, int id)
    {
        //PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
        lobbyCam.SetActive(false);
        lobbyUI.SetActive(false);
        if (id ==0)
        {
            PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity);
        }
        else if (id ==1)
        {
            PhotonNetwork.Instantiate(playerPrefab2.name, spawnPos, Quaternion.identity);
            //need to set different cameras
            //https://answers.unity.com/questions/1305391/how-do-you-give-each-player-a-separate-camera.html
        }
    }


}
