using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeImageEffect : MonoBehaviour
{
    [SerializeField] private AnimationCurve _fadeCurve;
    [SerializeField] private float _fadeTime;
    [SerializeField] private bool _fadeIn;

    private Image _image;
    private Utils.Timer _timer;

    public bool Completed => _image.color.a == (_fadeIn ? 1f : 0f);

    private void Awake()
    {
        _image = GetComponent<Image>();
        if (_fadeIn)
        {
            Color currentColor = _image.color;
            _image.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);
        }
    }

    private void OnEnable()
    {
        _image = GetComponent<Image>();
        if (_fadeIn)
        {
            Color currentColor = _image.color;
            _image.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0f);
        }
    }

    private void Update()
    {
        if (_timer == null) return;

        Color currentColor = _image.color;
        if (_timer.HasPassed())
        {
            _image.color = new Color(currentColor.r, currentColor.g, currentColor.b, _fadeIn ? 1f : 0f);
            Destroy(this);
            return;
        }
        float currentValue = _fadeCurve.Evaluate(_timer.passedValue());

        _image.color = new Color(currentColor.r, currentColor.g, currentColor.b, _fadeIn ? currentValue : 1f - currentValue);
    }

    public void TriggerFade()
    {
        _timer = new Utils.Timer(_fadeTime);
    }
}
