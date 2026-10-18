using UnityEngine;

public class PlayerFuel : MonoBehaviour
{

    private PlayerMovement _player;
    private PlayerProperties _playerProperties;
    private GravityEffect _gravityEffect;
    private Utils.Timer _timer;

    void Start()
    {
        _player = GetComponent<PlayerMovement>();
        _playerProperties = GetComponent<PlayerProperties>();
        _gravityEffect = GetComponent<GravityEffect>();
    }

    void Update()
    {
        _playerProperties.currentFuel = Mathf.Clamp(_playerProperties.currentFuel, 0f, _playerProperties.MaxFuel);
        if (!_playerProperties.IsThrottling)
        {
            _timer = null;
            return;
        }
        if(_timer == null)
        {
            _timer = new Utils.Timer(_playerProperties.FuelConsumptionRate);
        }
        if(_timer.HasPassed())
        {
            ConsumeFuel();
            _timer = null;
            return;
        }
    }

    private bool ConsumeFuel()
    {
        if (!_playerProperties.HasFuel) return false;
        _playerProperties.currentFuel = Mathf.Max(_playerProperties.currentFuel - 1f, 0f);
        return true;
    }
}
