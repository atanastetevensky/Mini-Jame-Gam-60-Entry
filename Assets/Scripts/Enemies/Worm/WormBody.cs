using System.Linq;
using UnityEngine;

public class WormBody : MonoBehaviour
{
    private WormHead _head;
    private Rigidbody2D _headRb;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private float _distance;

    public Transform target;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _head = transform.parent.parent.GetComponent<WormHead>();
        _headRb = _head.GetComponent<Rigidbody2D>();
        spriteRenderer = _headRb.GetComponent<SpriteRenderer>();
        _distance = Vector2.Distance(transform.position, target.position);
        transform.parent = transform.parent.parent.parent;
    }

    private void FixedUpdate()
    {
        Vector2 positionDifference = transform.position - target.position;

        Vector2 direction = positionDifference.normalized;

        rb.MovePosition((Vector2)target.position + direction * _distance);
        if(_headRb.linearVelocity != Vector2.zero)
        {
            rb.rotation = Vector2.SignedAngle(Vector2.up, direction);
        }
    }
}
