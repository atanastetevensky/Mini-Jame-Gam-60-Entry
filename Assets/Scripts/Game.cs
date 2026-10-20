using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game instance;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private GameObject _gameOverMenu;
    public PlayerMovement Player => _player;
    public bool MainMenuActive = true;
    public bool gameOver = false;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(gameOver)
        {
            PlayerInput playerInput = _player.GetComponent<PlayerInput>();
            playerInput.DeactivateInput();
            _gameOverMenu.SetActive(true);
        }

        if (MainMenuActive) return;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
