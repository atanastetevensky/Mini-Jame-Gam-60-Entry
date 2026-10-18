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
        //_buttonEnableTimer = new Utils.Timer(_buttonEnableDelay);
        foreach (GameObject button in _buttons)
        {
            button.transform.parent.gameObject.SetActive(true);
        }
        _header.SetActive(true);
        _header.GetComponent<FadeEffect>().TriggerFade();
    }

    private void Update()
    {
        //if (!_buttonEnableTimer.HasPassed()) return;
        //foreach (GameObject button in _buttons)
        //{
        //    Debug.Log("Hello");
        //    if (button.GetComponent<FadeEffect>().Completed) break;
        //    Debug.Log("Hi");
        //    button.GetComponent<FadeEffect>().TriggerFade();
        //}
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
