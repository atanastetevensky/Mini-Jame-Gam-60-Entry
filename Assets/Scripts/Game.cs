using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance;
    [SerializeField] private PlayerMovement _player;
    [SerializeField] private GameObject _gameOverMenu;
    [SerializeField] private AudioSource _OrbitTheme;
    [SerializeField] private AudioSource _spaceTheme;
    [SerializeField] private float _themeTime;
    [SerializeField] private float _themeFadeSpeed;
    [SerializeField] private float _spaceThemeRepeatTime;
    [SerializeField] private float _spaceThemeFadeSpeed;

    private Utils.Timer _orbitThemeTimer;
    private Utils.Timer _spaceThemeTimer;
    public PlayerMovement Player => _player;
    public bool MainMenuActive = true;
    public bool gameOver = false;


    private void Awake()
    {
        Instance = this;
        _spaceThemeTimer = new Utils.Timer(_spaceThemeRepeatTime);
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

        if(_spaceThemeTimer != null)
        {
            if(_spaceThemeTimer.HasPassed())
            {
                _spaceTheme.Play();
                _spaceThemeTimer = new Utils.Timer(_spaceThemeRepeatTime);
            }
        }


        if(_orbitThemeTimer != null)
        {
            if(_orbitThemeTimer.HasPassed())
            {
                _OrbitTheme.volume -= _themeFadeSpeed;
            }
            return;
        }

        _orbitThemeTimer = new Utils.Timer(_themeTime);
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
