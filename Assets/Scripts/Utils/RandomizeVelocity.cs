using UnityEngine;

public class RandomizeVelocity : MonoBehaviour
{
    [SerializeField] private Vector2 _minVelocity;
    [SerializeField] private Vector2 _maxVelocity;

    private Rigidbody2D _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        float horizontal = Random.Range(-_maxVelocity.x, _maxVelocity.x);
        float vertical = Random.Range(-_maxVelocity.y, _maxVelocity.y);

        _rb.linearVelocity = new Vector2(horizontal, vertical);
    }
}
