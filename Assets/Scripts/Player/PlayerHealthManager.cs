using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] GameObject _explosionObjectPrefab;
    [SerializeField] GameObject _outOfShipPrefab;
    [SerializeField] int _destroyShipFrame;

    private Rigidbody2D _rb;
    private PlayerProperties _properties;
    private Animator _animator;
    private GameObject _explosionObject;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _properties = GetComponent<PlayerProperties>();
        _animator = GetComponent<Animator>();
        _properties.currentHealth = _properties.MaxHealth;
    }

    private void Update()
    {
    }

    public void TakeDamage(float damageValue, DeathCause deathCause)
    {
        _properties.currentHealth -= damageValue;
        _properties.currentHealth = Mathf.Max(0f, _properties.currentHealth);
        if(_properties.currentHealth == 0) Death(deathCause);
    }

    public void Death(DeathCause deathCause)
    {
        if (Game.instance.gameOver) return;

        Game.instance.gameOver = true;

        switch (deathCause)
        {
            case DeathCause.ObstacleCrash:
                CrashDeath(true);
                break;

            case DeathCause.PlanetCrash:
                CrashDeath(false);
            break;

            case DeathCause.OutOfFuel:
                OutOfFuelDeath();
                break;
        }
    }

    private void CrashDeath(bool enablePhysics)
    {
        _rb.simulated = enablePhysics;

        _explosionObject = Instantiate(_explosionObjectPrefab, transform.position, Quaternion.identity, transform);
        Animator explosionAnimator = _explosionObject.GetComponent<Animator>();
        float frameTime = Utils.GetFrameTime(explosionAnimator);
        float removeShipTime = frameTime * _destroyShipFrame;

        new TimerManager.Timer(OnRemoveShipFrame, removeShipTime, false);
    }

    private void OnRemoveShipFrame()
    {
        GameObject outofShipInstance = Instantiate(_outOfShipPrefab, transform.position, Quaternion.identity, null);
        _explosionObject.transform.parent = outofShipInstance.transform;
        _explosionObject.transform.localPosition = Vector2.zero;
        Rigidbody2D outOfShipRb = outofShipInstance.GetComponent<Rigidbody2D>();
        outOfShipRb.linearVelocity = _rb.linearVelocity;
        outOfShipRb.angularVelocity = _rb.angularVelocity;
        this.gameObject.SetActive(false);
    }

    private void OutOfFuelDeath()
    {
        _animator.SetTrigger("InShipDeath");
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Grapple>().enabled = false;
    }


}
public enum DeathCause
{
    PlanetCrash,
    ObstacleCrash,
    OutOfFuel
}