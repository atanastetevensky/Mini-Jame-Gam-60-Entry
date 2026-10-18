using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    [SerializeField] GameObject _explosionObjectPrefab;
    
    private PlayerProperties _properties;
    private Animator _animator;

    private void Awake()
    {
        _properties = GetComponent<PlayerProperties>();
        _animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damageValue, DeathCause deathCause)
    {
        _properties.currentHealth -= damageValue;
        _properties.currentHealth = Mathf.Max(0f, _properties.currentHealth);
        if(_properties.currentHealth == 0) Death(deathCause);
    }

    public void Death(DeathCause deathCause)
    {
        Game.Instance.gameOver = true;

        //_properties.GetComponent<PlayerMovement>().enabled = false;
        //_properties.GetComponent<Grapple>().enabled = false;

        //switch(deathCause)
        //{
        //    case DeathCause.Crash:
        //        CrashDeath();
        //        break;

        //    case DeathCause.OutOfFuel:
        //        OutOfFuelDeath();
        //        break;
        //}
    }

    private void CrashDeath()
    {
        Rigidbody2D playerRb = _properties.GetComponent<Rigidbody2D>();
        playerRb.linearVelocity = Vector3.zero;
        GameObject explosionInstance = Instantiate(_explosionObjectPrefab, transform.position, Quaternion.identity, transform);
        Animator explosionAnimator = explosionInstance.GetComponent<Animator>();

        //explosionAnimator.clip
    }

    private void OutOfFuelDeath()
    {

    }

    public enum DeathCause
    {
        OutOfFuel,
        Crash
    }
}
