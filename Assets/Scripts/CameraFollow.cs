using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float lagTime;

    Vector3 velocity;

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, (Vector3)target.position + offset, ref velocity, lagTime);
    }
}
