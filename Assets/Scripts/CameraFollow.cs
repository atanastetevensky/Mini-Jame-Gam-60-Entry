using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _planetOffset;
    [SerializeField] private float _lagTime;
    [SerializeField] private float _planetRotateSpeed;

    private GravityEffect _gravityEffect;

    Vector3 rotationVelocity;
    Vector3 velocity;

    private void Start()
    {
        _gravityEffect = _player.GetComponent<GravityEffect>();
    }

    void LateUpdate()
    {
        Vector2 targetPosition = _player.position;
        Vector3 currentOffset = _offset;
        float currentLag = _lagTime;
        Quaternion targetRotation = Quaternion.identity;

        if (_gravityEffect.isInOrbit)
        {
            targetPosition = _gravityEffect.currentPlanet.transform.position;
            Vector2 playerDirection = ((Vector2)_player.transform.position - targetPosition).normalized;
            currentOffset = (_planetOffset + _gravityEffect.currentPlanet.planetRadius) * playerDirection;
            currentOffset.z = -10f;

            currentLag = 0f;

            targetRotation = Quaternion.LookRotation(Vector3.forward, playerDirection);
        }

        transform.position = Vector3.SmoothDamp(transform.position, (Vector3)targetPosition + currentOffset, ref velocity, _lagTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f - Mathf.Exp(-_planetRotateSpeed * Time.deltaTime));
    }
}
