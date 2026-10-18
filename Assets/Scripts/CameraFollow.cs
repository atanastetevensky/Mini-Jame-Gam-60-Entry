using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _projectionSize;
    [SerializeField] private float _orbitProjectionSize;
    [SerializeField] private float _planetOffset;
    [SerializeField] private float _lagTime;
    [SerializeField] private float _planetRotateSpeed;
    [SerializeField] private float _mainMenuProjectionSize;
    [SerializeField] private Vector3 _mainMenuOffset;
    [SerializeField] private float _menuOffsetReturnSpeed;
    [SerializeField] private float _menuOffsetReturnAcceleration;
    [SerializeField] private float _menuZoomOutMaxSpeed;
    [SerializeField] private float _menuZoomOutAcceleration;

    private GravityEffect _gravityEffect;
    private Camera _camera;
    private Vector3 _currentMainMenuOffset;
    private float _currentMenuOffsetReturnSpeed = 0f;
    private float _currentMenuZoomOutSpeed = 0f;

    Vector3 rotationVelocity;
    Vector3 velocity;

    private void Start()
    {
        _gravityEffect = _player.GetComponent<GravityEffect>();
        _camera = GetComponent<Camera>();
        transform.position = _player.position;
        if(Game.Instance.MainMenuActive)
        {
            transform.position += (Vector3) _mainMenuOffset;
            _camera.orthographicSize = _mainMenuProjectionSize;
        }
    }

    void LateUpdate()
    {
        float currentProjectionSize = _gravityEffect.isInOrbit ? _orbitProjectionSize : _projectionSize;
        if(Game.Instance.MainMenuActive)
        {
            currentProjectionSize = _mainMenuProjectionSize;
        }
        if(_camera.orthographicSize != currentProjectionSize)
        {
            _currentMenuZoomOutSpeed = Mathf.MoveTowards(_currentMenuZoomOutSpeed, _menuZoomOutMaxSpeed, _menuZoomOutAcceleration);
            _camera.orthographicSize = Mathf.MoveTowards(_camera.orthographicSize, currentProjectionSize, _currentMenuZoomOutSpeed);
        }

        Vector3 targetPosition = _player.position;
        Vector3 currentOffset = _offset;
        float currentLag = _lagTime;
        Quaternion targetRotation = Quaternion.identity;

        if (_gravityEffect.isInOrbit)
        {
            targetPosition = _gravityEffect.currentPlanet.transform.position;
            Vector2 playerDirection = (_player.transform.position - targetPosition).normalized;
            currentOffset = (_planetOffset + _gravityEffect.currentPlanet.planetRadius) * playerDirection;
            currentOffset.z = -10f;

            currentLag = 0f;

            targetRotation = Quaternion.LookRotation(Vector3.forward, playerDirection);
        }

        if(Game.Instance.MainMenuActive)
        {
            _currentMainMenuOffset = _mainMenuOffset;
        }
        targetPosition += _currentMainMenuOffset;
        _currentMenuOffsetReturnSpeed = Mathf.MoveTowards(_currentMenuOffsetReturnSpeed, _menuOffsetReturnSpeed, _menuOffsetReturnAcceleration);
        _currentMainMenuOffset = Vector2.MoveTowards(_currentMainMenuOffset, Vector2.zero, _currentMenuOffsetReturnSpeed);

        transform.position = Vector3.SmoothDamp(transform.position, (Vector3)targetPosition + currentOffset, ref velocity, _lagTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f - Mathf.Exp(-_planetRotateSpeed * Time.deltaTime));
    }
}
