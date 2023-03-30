//using RiptideNetworking;
//using RiptideNetworking.Utils;
using Riptide;
using Riptide.Utils;
using System;
using UnityEngine;

public enum ServerToClientId : ushort
{
    playerSpawned = 1,
    input,
    cameras,
    inputSimple,
    aiUpdate,
    puzzleInteraction,
}

public enum ClientToServerId : ushort
{
    name = 1,
    input,
    pipePuzzleFinish,
    typingPuzzleFinish,
    distraction,
}

public class NetworkManagerClient : MonoBehaviour
{
    private static NetworkManagerClient _singleton;

    bool serverConnected = false;

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

    [SerializeField] private string ip;
    [SerializeField] private ushort port;

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60; //stops it going too fast
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        //subscribe to events
        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.ClientDisconnected += PlayerLeft;
        Client.Disconnected += DidDisconnect;
        
    }
    private void SetupAI()
    {
        var ais = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i=0; i< ais.Length; i++)
        {
            ais[i].GetComponent<AINetworking>().SetTarget();
        }
    }

    private void FixedUpdate()
    {
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
        if (GameObject.FindGameObjectWithTag("Player") != null && !serverConnected)
        {
            serverConnected = true;
            SetupAI();
        }
    }

    private void OnApplicationQuit()
    {
        Client.Disconnect();
    }

    public void Connect()
    {
        Client.Connect($"{ip}:{port}");
    }

    private void DidConnect(object sender, EventArgs e)
    {
        UIManager.Singleton.SendName();
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
    }

    private void DidDisconnect(object sender, EventArgs e)
    {
        UIManager.Singleton.BackToMain();
        foreach (PlayerClient player in PlayerClient.list.Values)
            Destroy(player.gameObject);
    }
    
   /*  public void EstimateClientServerStartTick(int serverTick)
    {
        serverEstimatedTick = Mathf.RoundToInt(serverTick + ((Client.RTT / 2) / 20));
        clientPredictedTick = Mathf.RoundToInt(serverTick + ((Client.RTT / 2) / 20) * 2);
        receivedServerStartTick = true;
    }
     public int EstimateServerTick(int serverTick)
    {
        if (serverEstimatedTick > serverTick)
            return serverTick;

        int serverCalculatedTick = Mathf.RoundToInt(serverTick + ((Client.RTT / 2) / 20));
        if (serverEstimatedTick != serverCalculatedTick)
        {
            return serverCalculatedTick;
        } else
        {
            return serverTick;
        }
    }
     public void ResetTicks()
    {
        receivedServerStartTick = false;
        clientPredictedTick = 0;
        serverEstimatedTick = 0;
        DelayTick = 0;
    } */
}