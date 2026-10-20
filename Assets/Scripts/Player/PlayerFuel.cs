using UnityEngine;
using static UnityEngine.Audio.GeneratorInstance;

public class PlayerFuel : MonoBehaviour
{

    private PlayerMovement _player;
    private PlayerProperties _playerProperties;
    private GravityEffect _gravityEffect;
    private TimerManager.Timer _consumeTimer;

    void Start()
    {
        _player = GetComponent<PlayerMovement>();
        _playerProperties = GetComponent<PlayerProperties>();
        _gravityEffect = GetComponent<GravityEffect>();

        _playerProperties.currentFuel = _playerProperties.MaxFuel;


        _consumeTimer = new TimerManager.Timer(OnTimerPass, _playerProperties.FuelConsumptionRate, true);
    }

    void Update()
    {
        _playerProperties.currentFuel = Mathf.Clamp(_playerProperties.currentFuel, 0f, _playerProperties.MaxFuel);

        if(!_playerProperties.HasFuel)
        {
            PlayerHealthManager playerHealth = GetComponent<PlayerHealthManager>();
            playerHealth.Death(DeathCause.OutOfFuel);
        }
    }

    private void OnTimerPass()
    {
        if (!_playerProperties.IsThrottling) return;
        ConsumeFuel();
    }

    private void ConsumeFuel()
    {
        _playerProperties.currentFuel = Mathf.Max(_playerProperties.currentFuel - 1f, 0f);
    }

    public void Refuel(float fuelAmount)
    {
        _playerProperties.currentFuel += fuelAmount;
        _playerProperties.currentFuel = Mathf.Min(_playerProperties.MaxFuel, _playerProperties.currentFuel);
    }

    public void FullRefuel()
    {
        Refuel(_playerProperties.MaxFuel);
    }
}
