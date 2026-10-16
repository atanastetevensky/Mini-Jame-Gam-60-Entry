using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerProperties properties { get; private set; }

    private Rigidbody2D rb;


    void Start()
    {
        properties = GetComponent<PlayerProperties>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        

        if (properties.HasGravity)
        {
            ApplyGravity();
        }


        if(properties.isThrottling)
        {
            properties.currentThrottleDirection = Quaternion.Euler(0f, 0f, rb.rotation) * Vector2.up;

            properties.currentThrottleSpeed += properties.Acceleration;
            properties.currentThrottleSpeed = Mathf.Min(properties.currentThrottleSpeed, properties.MaxThrottleSpeed);
        } else
        {
            properties.currentThrottleSpeed = Mathf.MoveTowards(properties.currentThrottleSpeed, 0f, properties.VelocityDissipation);
        }

        Vector2 targetThrottleVelocity = properties.currentThrottleDirection * properties.currentThrottleSpeed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetThrottleVelocity, properties.ThrottleDrag);


        properties.currentTurnSpeed += properties.TurnAcceleration * properties.SteerInput;
        properties.currentTurnSpeed = Mathf.Clamp(properties.currentTurnSpeed, -properties.MaxTurnSpeed, properties.MaxTurnSpeed);
        float targetRotation = rb.rotation - properties.currentTurnSpeed;
        rb.MoveRotation(targetRotation);

        if(properties.SteerInput == 0)
        {
            properties.currentTurnSpeed = Mathf.MoveTowards(properties.currentTurnSpeed, 0f, properties.RotationDissipation);
        }
    }

    void OnMove(InputValue value)
    {
        properties.movementInput = value.Get<Vector2>();
    }

    private void ApplyGravity()
    {
        rb.AddForce(new Vector2(0f, -properties.Gravity));
    }


    private void HandleCollision(Collision2D collision)
    {
        Vector2 intended = (Vector2)transform.up * Mathf.Sign(properties.currentThrottleSpeed);

        float headOn = Vector2.Dot(intended, collision.GetContact(0).normal);

        if (headOn < -0.5f)
        {
            float realForwardSpeed = Vector2.Dot(rb.linearVelocity, transform.up);

            if (Mathf.Abs(properties.currentThrottleSpeed) > Mathf.Abs(realForwardSpeed))
                properties.currentThrottleSpeed = realForwardSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) => HandleCollision(collision);
    private void OnCollisionStay2D(Collision2D collision) => HandleCollision(collision);
}
