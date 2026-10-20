using System.Collections.Generic;
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
    [SerializeField] private float _orbitVerticalThrottleMultiplier;
    public float OrbitVerticalThrottleMultiplier => _orbitVerticalThrottleMultiplier;
    [SerializeField] private float _orbitHorizontalThrottleMultiplier;
    public float OrbitHorizontalThrottleMultiplier => _orbitHorizontalThrottleMultiplier;
    [SerializeField] private float _maxHealth;
    public float MaxHealth => _maxHealth;
    [SerializeField] private LayerMask _crashLayers;
    public LayerMask CrashLayers => _crashLayers;
    [SerializeField] private float _crashSpeedThreshold;
    public float CrashSpeedThreshold => _crashSpeedThreshold;
    [SerializeField] private float _maxFuel;
    public float MaxFuel => _maxFuel;
    [SerializeField] private float _fuelConsumptionRate;
    public float FuelConsumptionRate => _fuelConsumptionRate;
    [SerializeField] private float _startingPoints;
    public float StartingPoints => _startingPoints;

    [Header("READ ONLY")]
    [Space(5)]
    public Vector2 movementInput = Vector2.zero;
    public float currentThrottleSpeed = 0f;
    public float currentTurnSpeed = 0f;
    public Vector2 currentThrottleDirection;
    public float SteerInput => movementInput.x;
    public bool IsThrottling => movementInput.y == 1;
    public bool HasFuel => currentFuel > 0;

    [Space(5)]
    public float currentHealth;
    public float currentFuel = 0;
    public float currentPoints = 0;
}
