using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField] private Player _player;
    public Player GetPlayer => _player;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
