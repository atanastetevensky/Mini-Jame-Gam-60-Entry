using UnityEngine;

public class RandomizeAngularVelocity : MonoBehaviour
{
    [SerializeField] private float _velocityRange;

    private Rigidbody2D _rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        float rotation = 0f;

        while(Mathf.Abs(rotation) < 10)
        {
            rotation = Random.Range(-_velocityRange, _velocityRange);
        }

        _rb.angularVelocity = rotation;
    }
}
