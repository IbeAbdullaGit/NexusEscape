using UnityEngine;

public class GameLogicClient : MonoBehaviour
{
    private static GameLogicClient _singleton;
    public static GameLogicClient Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(GameLogicClient)} instance already exists, destroying duplicate!");
                Destroy(value);
            }
        }
    }

    public GameObject Player2Prefab => player2Prefab;
    public GameObject Player1Prefab => player1Prefab;

    [Header("Prefabs")]
    [SerializeField] private GameObject player2Prefab;
    [SerializeField] private GameObject player1Prefab;

    private void Awake()
    {
        Singleton = this;
    }
}