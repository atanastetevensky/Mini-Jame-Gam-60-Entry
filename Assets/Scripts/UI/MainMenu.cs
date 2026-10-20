using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private FadeImageEffect _titleEffect;
    [SerializeField] private FadeEffect[] _buttonsEffect;
    [SerializeField] private GameObject _ingameUI;

    private PlayerInput _playerInput;
    private bool _pressedPlay;

    private void Start()
    {
        _playerInput = Game.instance.Player.GetComponent<PlayerInput>();
        _playerInput.DeactivateInput();
    }

    private void Update()
    {
        if (!_pressedPlay) return;
        if(_titleEffect.Completed)
        {
            _playerInput.ActivateInput();
            _ingameUI.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    public void OnPlay()
    {
        _pressedPlay = true;

        Game.instance.MainMenuActive = false;

        _titleEffect.TriggerFade();

        foreach (FadeEffect buttonEffect in _buttonsEffect)
        {
            buttonEffect.TriggerFade();
        }
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
