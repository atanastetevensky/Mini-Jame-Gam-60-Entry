using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] GameObject _explosionObjectPrefab;
    [SerializeField] GameObject _outOfShipPrefab;
    [SerializeField] int _destroyFrame;

    private Rigidbody2D _rb;
    private PlayerProperties _properties;
    private Animator _animator;
    private Animator _explosionAnimator;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _properties = GetComponent<PlayerProperties>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_explosionAnimator == null) return;

        int currentFrame = Utils.getCurrentFrameIndex(_explosionAnimator);
        if (currentFrame != _destroyFrame) return;

        _explosionAnimator = null;

        GameObject outofShipInstance = Instantiate(_outOfShipPrefab, transform.position, Quaternion.identity, null);
        Rigidbody2D outOfShipRb = outofShipInstance.GetComponent<Rigidbody2D>();
        outOfShipRb.linearVelocity = _rb.linearVelocity;
        outOfShipRb.angularVelocity = _rb.angularVelocity;
        this.gameObject.SetActive(false);
    }

    public void TakeDamage(float damageValue, DeathCause deathCause)
    {
        _properties.currentHealth -= damageValue;
        _properties.currentHealth = Mathf.Max(0f, _properties.currentHealth);
        if(_properties.currentHealth == 0) Death(deathCause);
    }

    public void Death(DeathCause deathCause)
    {
        if (Game.Instance.gameOver) return;

        Game.Instance.gameOver = true;

        switch (deathCause)
        {
            case DeathCause.Crash:
                CrashDeath();
                break;

            case DeathCause.OutOfFuel:
                OutOfFuelDeath();
                break;
        }
    }

    private void CrashDeath()
    {
        GameObject explosionInstance = Instantiate(_explosionObjectPrefab, transform.position, Quaternion.identity, transform);
        _explosionAnimator = explosionInstance.GetComponent<Animator>();
    }

    private void OutOfFuelDeath()
    {
        _animator.SetTrigger("InShipDeath");
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Grapple>().enabled = false;
    }

    public enum DeathCause
    {
        OutOfFuel,
        Crash
    }

}
