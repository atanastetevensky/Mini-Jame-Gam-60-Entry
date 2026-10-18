using UnityEngine;

public class ThrottleEffect : MonoBehaviour
{
    [SerializeField] private PlayerProperties _playerProperties;
    [SerializeField] private AudioSource _throttleSound;
    [SerializeField] private AudioSource _releaseSound;
    private SpriteRenderer _renderer;
    private bool _currentThrottling;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (_currentThrottling == _playerProperties.IsThrottling) return;
        _currentThrottling = _playerProperties.IsThrottling;
        _renderer.enabled = _currentThrottling;
        if(_currentThrottling)
        {
            _throttleSound.Play();
            return;
        }
        _throttleSound.Stop();
        _releaseSound.Play();
    }
}
