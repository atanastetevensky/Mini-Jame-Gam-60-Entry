using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private GameObject _gameOverMenu;
    public PlayerMovement Player => _player;
    public bool MainMenuActive = true;
    public bool gameOver = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(gameOver)
        {
            _gameOverMenu.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
