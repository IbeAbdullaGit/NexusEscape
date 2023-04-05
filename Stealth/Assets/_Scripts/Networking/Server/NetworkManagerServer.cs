using Riptide;
using Riptide.Utils;
using UnityEngine;
using System.Net.Sockets;
using System.Net;



/* public enum ServerToClientId : ushort
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
} */

public class NetworkManagerServer : MonoBehaviour
{
    private static NetworkManagerServer _singleton;

    bool connected = false;
    bool spawned = false;
    public static NetworkManagerServer Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(NetworkManagerServer)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    public Server Server { get; private set; }

    [SerializeField] public ushort port;
    [SerializeField] public ushort maxClientCount;

    private void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //we dont need in start
        
        /* Application.targetFrameRate = 60; //stops it going too fast

        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.Start(port, maxClientCount);
        Server.ClientDisconnected += PlayerLeft;
        Server.ClientConnected += PlayerJoin;

        Debug.Log("Started server"); */
        
    }
    public static string GetPublicIP()
    {
        string url = "http://checkip.dyndns.org";
        System.Net.WebRequest req = System.Net.WebRequest.Create(url);
        System.Net.WebResponse resp = req.GetResponse();
        System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
        string response = sr.ReadToEnd().Trim();
        string[] a = response.Split(':');
        string a2 = a[1].Substring(1);
        string[] a3 = a2.Split('<');
        string a4 = a3[0];
        return a4;
    }

    public void StartServer()
    {
        Application.targetFrameRate = 60; //stops it going too fast

        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.Start(port, maxClientCount);
        
        Server.ClientDisconnected += PlayerLeft;
       // Server.ClientConnected += PlayerJoin;
       Server.ClientConnected += CanStartGame;
        

        



        Debug.Log("Started server");

        connected = true;
    }

    private void FixedUpdate()
    {
        if (connected)
            Server.Update();
    }
    private void Update() {
        //if (connected)
            //Server.Update();
    }

    private void OnApplicationQuit()
    {
        //if (connected)
            Server.Stop();
    }

    private void PlayerJoin(object sender, ServerConnectedEventArgs e)
    {
        //spawn our player
        PlayerServer player = Instantiate(GameLogic.Singleton.PlayerPrefab, GameLogic.Singleton.PlayerPrefab.transform.position, Quaternion.identity).GetComponent<PlayerServer>();

        player.name = "Player hacker";
        player.Id = 2;
        player.Username = "hacker";

        
        player.SendSpawned();
        //PlayerServer.list.Add(2, player);

        //send initial cameras
        //SendCameras();

        SetupAI();
        
    }
    private void CanStartGame(object sender, ServerConnectedEventArgs e)
    {
        //assume we will only see this at the start of the game when we connect and have the lobby component
        //the rest will be handled by lobby script, all we need is this button to be available
        if (GetComponent<Lobby>())
            GetComponent<Lobby>().startButton.interactable = true;

        if (GameObject.FindGameObjectWithTag("GameController").GetComponent<Lobby>())
            GameObject.FindGameObjectWithTag("GameController").GetComponent<Lobby>().startButton.interactable = true;

        //nexus 1
        if (InteractionServerNexus1.Singleton!= null && !spawned)
        {
            //do spawn
            PlayerServer player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerServer>();

            player.name = "Player ground";
            player.Id = 2;
            player.Username = "ground";

            //send spawn to other player, and this should send movement messages

            player.SendSpawned();

            Debug.Log("Sending spawn message");
            spawned = true;
        }
        //nexus 2
        if (InteractionMessages.Singleton != null && !spawned)
        {
            //do spawn
            PlayerServer player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerServer>();

            player.name = "Player ground";
            player.Id = 2;
            player.Username = "ground";

            //send spawn to other player, and this should send movement messages

            player.SendSpawned();

            Debug.Log("Sending spawn message");
            spawned = true;
        }
    }
    private void SetupAI()
    {
        var ais = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i=0; i< ais.Length; i++)
        {
            ais[i].GetComponent<EnemyAI>().SetTarget();
        }
    }
    private void SendCameras()
    {
        Message message = Message.Create(MessageSendMode.Reliable, ServerToClientId.cameras);
        //what we need to send: the materials, the cameras
        //get the camera menu component
        var cams = GameObject.FindGameObjectWithTag("GameController").GetComponent<CameraMenu>();

        //first send how many cams there are
        message.AddInt(cams.cameras.Length);
  
        //materials first
        //need to copy content, we just get texture for now, will apply to material on client side
        for (int i=0; i< cams.cameras.Length; i++)
        {
            Texture mainTex = cams.cameras[i].mainTexture;
            Texture2D texture2D = new Texture2D(mainTex.width, mainTex.height, TextureFormat.RGBA32, false);
            //maybe better way to get this?
            RenderTexture currentRT = RenderTexture.active;
           
            RenderTexture renderTex = new RenderTexture(mainTex.width, mainTex.height, 32);
            Graphics.Blit(mainTex, renderTex);
            RenderTexture.active = renderTex;
            texture2D.ReadPixels(new Rect(0, 0, mainTex.width, mainTex.height), 0, 0);
            texture2D.Apply();
            
            message.AddBytes(texture2D.EncodeToPNG());
        }
        //resend texture every time button is pressed? so dont need to send cams
        //also send minimap texture


        NetworkManagerServer.Singleton.Server.SendToAll(message);
    }
    private void PlayerLeft(object sender, ServerDisconnectedEventArgs e)
    {
       //Destroy(PlayerServer.list[e.Client.Id].gameObject);
       //try to connect again
      // NetworkManagerClient.Singleton.Connect();
    }
}