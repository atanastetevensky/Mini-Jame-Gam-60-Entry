using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerProperties _properties { get; private set; }

    private GravityEffect _gravity;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _properties = GetComponent<PlayerProperties>();
        _gravity = GetComponent<GravityEffect>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.angularDamping = _properties.RotationDissipation;
    }

    private void FixedUpdate()
    {
        /*Since throttleDrag (affecting how fast throttle can change your trajectory) and velocityDissipation (affecting velocity loss when stopped) are directly timed,
         we're switching between the 2 for better control.*/
        _rb.linearDamping = _properties.IsThrottling ? _properties.ThrottleDrag : _properties.VelocityDissipation;

        //Throttle is very different within orbit. Horizontal Throttle needs to be decreased to allow _gravity to have proper effect, while vertical throttle needs to be increased to counter _gravity properly.
        float currentOrbitThrottleMultiplier = 1f;
        if (_gravity.isInOrbit)
        {
            float currentOrbitFacing = Mathf.Cos(_gravity.currentPlanet.RotationDifference(_gravity) * Mathf.Deg2Rad);
            float multiplierDifference = Mathf.Abs(_properties.OrbitVerticalThrottleMultiplier - _properties.OrbitHorizontalThrottleMultiplier);
            currentOrbitThrottleMultiplier = Mathf.Min(_properties.OrbitVerticalThrottleMultiplier, _properties.OrbitHorizontalThrottleMultiplier);
            currentOrbitThrottleMultiplier += multiplierDifference * Mathf.Abs(currentOrbitFacing);
        }
        //Handles throttling behaviour
        if (_properties.IsThrottling)
        {
            _properties.currentThrottleDirection = Quaternion.Euler(0f, 0f, _rb.rotation) * Vector2.up;

            _rb.AddForce(_properties.currentThrottleDirection * _properties.Acceleration * currentOrbitThrottleMultiplier + _gravity.currentGravity);
        }
        else
        {
            _rb.AddForce(_gravity.currentGravity);
        }
        _rb.linearVelocity = Vector2.ClampMagnitude(_rb.linearVelocity, _properties.MaxThrottleSpeed);

        //Handles turn behaviour
        //_properties.currentTurnSpeed += _properties.TurnAcceleration * _properties.SteerInput;
        //_properties.currentTurnSpeed = Mathf.Clamp(_properties.currentTurnSpeed, -_properties.MaxTurnSpeed, _properties.MaxTurnSpeed);
        //float targetRotation = _rb.rotation - _properties.currentTurnSpeed;

        _rb.AddTorque(-_properties.TurnAcceleration * _properties.SteerInput);
        _rb.rotation += _gravity.orbitDelta;
        _rb.angularVelocity = Mathf.Clamp(_rb.angularVelocity, -_properties.MaxTurnSpeed, _properties.MaxTurnSpeed);

        if (_properties.SteerInput == 0)
        {
            _properties.currentTurnSpeed = Mathf.MoveTowards(_properties.currentTurnSpeed, 0f, _properties.RotationDissipation);
        }
    }

    public void Pushback(Vector2 direction,float pushbackAmount, float torque)
    {
        if (pushbackAmount == 0) return;
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        _rb.AddForce(direction * pushbackAmount);
        _rb.AddTorque(torque);
    }

    void OnMove(InputValue value)
    {
        _properties.movementInput = value.Get<Vector2>();
    }
}
