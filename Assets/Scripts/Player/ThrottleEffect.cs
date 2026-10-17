using UnityEngine;

public class ThrottleEffect : MonoBehaviour
{
    [SerializeField] private PlayerProperties _playerProperties;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(_renderer.enabled != _playerProperties.isThrottling)
        {
            _renderer.enabled = _playerProperties.isThrottling;
        }
    }
}
