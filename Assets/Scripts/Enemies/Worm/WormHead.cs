using System.Collections.Generic;
using UnityEngine;

public class WormHead : MonoBehaviour
{
    [field: SerializeField] public float maxSpeed {  get; private set; }
    [SerializeField] private float _acceleration;
    [SerializeField] private float _roamRange; //a radius under which the worm will remain when in roaming state
    [SerializeField] private float _directionChangeFrequency; //measured in seconds. How long before worm changes direction
    [SerializeField] private int _wormLength; //how many wormBody instances will spawn behind the head
    [SerializeField] private float _distanceToBody; //offset value for how far away the body will trail behind the head
    [field: SerializeField] public float bodyPartDistance {  get; private set; } //distance between body parts
    [SerializeField] private Transform _headJoint; //the joint that the body behind it will actually attach to (a child object under the head)
    [SerializeField] private GameObject _bodyPrefab;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 _currentDirection = Vector2.up;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = _wormLength;

        new TimerManager.Timer(ChangeDirection, _directionChangeFrequency, true);

        Transform lastTarget = _headJoint;

        for (int i = 1; i <= _wormLength; i++)
        {
            Vector2 bodyLocalPosition = new Vector2(0f, (-bodyPartDistance * i) - _distanceToBody) * transform.localScale.x;
            WormBody instance = Instantiate(_bodyPrefab, _headJoint).GetComponent<WormBody>();
            SpriteRenderer instanceRenderer = instance.GetComponent<SpriteRenderer>();
            instance.transform.localPosition = bodyLocalPosition;
            instanceRenderer.sortingOrder = _wormLength - i;
            instance.target = lastTarget;
            lastTarget = instance.transform;
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(_acceleration * _currentDirection);
        rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);

        rb.rotation = Vector2.SignedAngle(Vector2.up, rb.linearVelocity);

        
    }

    private void ChangeDirection()
    {
        _currentDirection = Utils.RandomDirection();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.07f);
        Gizmos.DrawWireSphere(transform.parent.position, _roamRange);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * 3f);
    }
}
