using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private PlayerProperties _properties;

    private void Awake()
    {
        _properties = GetComponent<PlayerProperties>();
    }

    public void TakeDamage(float damageValue)
    {
        _properties.currentHealth -= damageValue;
        _properties.currentHealth = Mathf.Max(0f, _properties.currentHealth);
        if(_properties.currentHealth == 0) Death();
    }

    public void Death()
    {
        Game.Instance.gameOver = true;
    }
}
