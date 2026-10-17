using UnityEngine;

public class PlayerFuel : MonoBehaviour
{

    private Player _player;
    private PlayerProperties _playerProperties;
    private GravityEffect _gravityEffect;
    private Utils.Timer _timer;

    void Start()
    {
        _player = GetComponent<Player>();
        _playerProperties = GetComponent<PlayerProperties>();
        _gravityEffect = GetComponent<GravityEffect>();
    }

    private void FixedUpdate()
    {
        if (!_playerProperties.isThrottling)
        {
            _timer = null;
            return;
        }
        if(_timer == null)
        {
        }
    }
}
