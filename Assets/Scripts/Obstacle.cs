using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float _damageValue;
    [SerializeField] private float _pushbackValue;


    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contactPoint = collision.GetContact(0);

        PlayerHealthManager playerHealth = collision.collider.GetComponent<PlayerHealthManager>();
        PlayerMovement playerMovement = collision.collider.GetComponent<PlayerMovement>();
        Rigidbody2D rb = collision.collider.attachedRigidbody;

        Debug.Log($"Collided with: {collision.collider.gameObject.name}");

        if (playerHealth == null) return;

        playerHealth.TakeDamage(_damageValue, PlayerHealthManager.DeathCause.Crash);

        Vector2 pushbackDirection = -contactPoint.normal;
        Vector2 pushbackForce = pushbackDirection * _pushbackValue;

        Vector2 radius = contactPoint.point - rb.worldCenterOfMass;
        float torque = radius.x * pushbackForce.y - radius.y * pushbackForce.x;

        playerMovement.Pushback(pushbackDirection, _pushbackValue, torque);

        Destroy(gameObject);
    }
}
