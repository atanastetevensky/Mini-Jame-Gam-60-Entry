using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField] private PlayerMovement _player;
    public PlayerMovement GetPlayer => _player;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
