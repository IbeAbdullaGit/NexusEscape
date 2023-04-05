//using RiptideNetworking;
//using RiptideNetworking.Utils;
using Riptide;
using Riptide.Utils;
using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;

public enum ServerToClientId : ushort
{
    playerSpawned = 1,
    input,
    cameras,
    inputSimple,
    aiUpdate,
    puzzleInteraction,
    startGame,
    resetGame,
    chatMessage,
}

public enum ClientToServerId : ushort
{
    name = 1,
    input,
    pipePuzzleFinish,
    typingPuzzleFinish,
    distraction,
    testMessage,
    chatMessage,
}

public class NetworkManagerClient : MonoBehaviour
{
    private static NetworkManagerClient _singleton;

    public bool serverConnected = false;

    bool connected = false;

    public static NetworkManagerClient Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(NetworkManagerClient)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    public Client Client { get; private set; }

    [SerializeField] public string ip;
    [SerializeField] public ushort port;

    private void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(gameObject);

        //get ip
        string localIP;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint.Address.ToString();
        }
        ip = localIP;
        Debug.Log("Ip address: " + ip);
    }
 

    private void Start()
    {
        /* Application.targetFrameRate = 60; //stops it going too fast
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        //subscribe to events
        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.ClientDisconnected += PlayerLeft;
        Client.Disconnected += DidDisconnect; */


        
    }
    public void StartClient()
    {
        Application.targetFrameRate = 60; //stops it going too fast
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        //subscribe to events
        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.ClientDisconnected += PlayerLeft;
        Client.Disconnected += DidDisconnect;
        
        Singleton = this;
        
        connected = true;

               
    }
    public void SetupAI()
    {
        var ais = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i=0; i< ais.Length; i++)
        {
            ais[i].GetComponent<EnemyAI>().SetTarget();
        }
        Debug.Log("Setup AI");
    }

    private void FixedUpdate()
    {
        if (connected)
          Client.Update();
       /*  if (receivedServerStartTick)
        {
            serverEstimatedTick++;
            clientPredictedTick++;
            DelayTick = serverEstimatedTick - 5 - ((Client.RTT / 2) / 20);
            if (DelayTick < 0)
            {
                DelayTick = 0;
            }
        } */
    }
    private void Update()
    {
        //if (GameObject.FindGameObjectWithTag("Player") != null && !serverConnected && connected)
        //{
        //    serverConnected = true;
        //    SetupAI();
        //}
        //if (connected)
          // Client.Update();
       
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    public void Connect()
    {
        Client.Connect($"{ip}:{port}");
       //Client.Connection.CanTimeout = false;
    }

    private void DidConnect(object sender, EventArgs e)
    {
        //UIManager.Singleton.SendName();
        //disable timeouts
        Client.Connection.CanTimeout = false;
        Debug.Log("Connected");
        
        //activae AI
        //EnemyNetworkManager.Singleton.ActivateAIs();

    }

    private void FailedToConnect(object sender, EventArgs e)
    {
        UIManager.Singleton.BackToMain();
    }

    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        if (PlayerClient.list.TryGetValue(e.Id, out PlayerClient player))
            Destroy(player.gameObject);

        Connect();
    }

    private void DidDisconnect(object sender, EventArgs e)
    {
        //UIManager.Singleton.BackToMain();
        //foreach (PlayerClient player in PlayerClient.list.Values)
            //Destroy(player.gameObject);

        Debug.Log("Disconnected");
        //try connecting again
        Connect();
    }
    
   #region Messages
   [MessageHandler((ushort)ServerToClientId.startGame)]
   private static void StartGame(Message message)
   {
        //the contents of the message don't really matter, just this signal to start
        //we should also have thhe UIManager and such attached at this point
        //we are connecting specifically to nexus 1 client, there are no other cases

        Debug.Log("Starting Game");
        //NetworkManagerClient.Singleton.Client.Disconnect();
        //get game manager
        GameObject.FindGameObjectWithTag("GameController").GetComponent<SwitchScene>().ChangeScene("Nexus1Client");
        //Client.Connection.ResetTimeout();
        //NetworkManagerClient.Singleton.GetComponent<SwitchScene>().ChangeScene("Nexus1Client");
   }
   #endregion
}