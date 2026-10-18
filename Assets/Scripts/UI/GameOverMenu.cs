using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject _header;
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private float _buttonEnableDelay;

    private Utils.Timer _buttonEnableTimer;

    private void OnEnable()
    {
        _buttonEnableTimer = new Utils.Timer(_buttonEnableDelay);
        _header.SetActive(true);
    }

    private void Update()
    {
        if (!_buttonEnableTimer.HasPassed()) return;
        if (_buttons[0].activeSelf) return;
        foreach (GameObject button in _buttons)
        {
            button.SetActive(true);
        }
    }

    public void OnRetry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void OnQuit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
