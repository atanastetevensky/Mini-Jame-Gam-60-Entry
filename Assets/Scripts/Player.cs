using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerProperties properties { get; private set; }

    private GravityEffect gravity;
    private Rigidbody2D rb;


    void Start()
    {
        properties = GetComponent<PlayerProperties>();
        gravity = GetComponent<GravityEffect>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        /*Since throttleDrag (affecting how fast throttle can change your trajectory) and velocityDissipation (affecting velocity loss when stopped) are directly timed,
         we're switching between the 2 for better control.*/
        rb.linearDamping = properties.isThrottling ? properties.ThrottleDrag : properties.VelocityDissipation;

        //Handles throttling behaviour
        if(properties.isThrottling)
        {
            properties.currentThrottleDirection = Quaternion.Euler(0f, 0f, rb.rotation) * Vector2.up;

            rb.AddForce(properties.currentThrottleDirection * properties.Acceleration);
        }
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, properties.MaxThrottleSpeed);

        //Handles turn behaviour
        properties.currentTurnSpeed += properties.TurnAcceleration * properties.SteerInput;
        properties.currentTurnSpeed = Mathf.Clamp(properties.currentTurnSpeed, -properties.MaxTurnSpeed, properties.MaxTurnSpeed);
        float targetRotation = rb.rotation - properties.currentTurnSpeed;
        rb.MoveRotation(targetRotation + gravity.orbitDelta);

        if(properties.SteerInput == 0)
        {
            properties.currentTurnSpeed = Mathf.MoveTowards(properties.currentTurnSpeed, 0f, properties.RotationDissipation);
        }
    }

    void OnMove(InputValue value)
    {
        properties.movementInput = value.Get<Vector2>();
    }
}
