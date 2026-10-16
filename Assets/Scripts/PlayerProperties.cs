using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [Header("ADJUSTABLE")]
    [Space(5)]
    [SerializeField] private float _maxThrottleSpeed;
    public float MaxThrottleSpeed => _maxThrottleSpeed;
    [SerializeField] private float _acceleration;
    public float Acceleration => _acceleration;
    [SerializeField] private float _throttleDrag;
    public float ThrottleDrag => _throttleDrag;
    [SerializeField] private float _maxTurnSpeed;
    public float MaxTurnSpeed => _maxTurnSpeed;
    [SerializeField] private float _turnAcceleration;
    public float TurnAcceleration => _turnAcceleration;
    [SerializeField] private float _velocityDissipation;
    public float VelocityDissipation => _velocityDissipation;
    [SerializeField] private float _rotationDissipation;
    public float RotationDissipation => _rotationDissipation;
    [SerializeField] private bool _hasGravity;
    public bool HasGravity => _hasGravity;
    [SerializeField] private float _gravity;
    public float Gravity => _gravity;

    [Header("READ ONLY")]
    [Space(5)]
    public Vector2 movementInput = Vector2.zero;
    public float currentThrottleSpeed = 0f;
    public float currentTurnSpeed = 0f;
    public Vector2 currentThrottleDirection;
    public float SteerInput => movementInput.x;
    public bool isThrottling => movementInput.y == 1;

}
