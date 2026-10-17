using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerProperties properties { get; private set; }

    private GravityEffect gravity;
    private Rigidbody2D rb;

    private void Awake()
    {
        properties = GetComponent<PlayerProperties>();
        gravity = GetComponent<GravityEffect>();
        rb = GetComponent<Rigidbody2D>();
        properties.currentPoints = properties.StartingPoints;
        properties.currentFuel = properties.MaxFuel;
    }

    void Start()
    {
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        /*Since throttleDrag (affecting how fast throttle can change your trajectory) and velocityDissipation (affecting velocity loss when stopped) are directly timed,
         we're switching between the 2 for better control.*/
        rb.linearDamping = properties.IsThrottling ? properties.ThrottleDrag : properties.VelocityDissipation;

        //Throttle is very different within orbit. Horizontal Throttle needs to be decreased to allow gravity to have proper effect, while vertical throttle needs to be increased to counter gravity properly.
        float currentOrbitThrottleMultiplier = 1f;
        if(gravity.isInOrbit)
        {
            float currentOrbitFacing = Mathf.Cos(gravity.currentPlanet.RotationDifference(gravity) * Mathf.Deg2Rad);
            float multiplierDifference = Mathf.Abs(properties.OrbitVerticalThrottleMultiplier - properties.OrbitHorizontalThrottleMultiplier);
            currentOrbitThrottleMultiplier = Mathf.Min(properties.OrbitVerticalThrottleMultiplier, properties.OrbitHorizontalThrottleMultiplier);
            currentOrbitThrottleMultiplier += multiplierDifference * Mathf.Abs(currentOrbitFacing);
        }
        //Handles throttling behaviour
        if(properties.IsThrottling)
        {
            properties.currentThrottleDirection = Quaternion.Euler(0f, 0f, rb.rotation) * Vector2.up;

            rb.AddForce(properties.currentThrottleDirection * properties.Acceleration * currentOrbitThrottleMultiplier + gravity.currentGravity);
        } else
        {
            rb.AddForce(gravity.currentGravity);
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
