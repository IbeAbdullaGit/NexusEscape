using Riptide;
using Riptide.Utils;
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
}

public class NetworkManagerServer : MonoBehaviour
{
    private static NetworkManagerServer _singleton;
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

    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientCount;

    private void Awake()
    {
        Singleton = this;
    }

    private void Start()
    {
        Application.targetFrameRate = 60; //stops it going too fast

        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.Start(port, maxClientCount);
        Server.ClientDisconnected += PlayerLeft;
        Server.ClientConnected += PlayerJoin;
        
    }

    private void FixedUpdate()
    {
        Server.Update();
    }

    private void OnApplicationQuit()
    {
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
       Destroy(PlayerServer.list[e.Client.Id].gameObject);
    }
}