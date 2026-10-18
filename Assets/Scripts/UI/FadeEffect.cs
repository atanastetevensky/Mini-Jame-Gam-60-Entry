using TMPro;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    [SerializeField] private AnimationCurve _fadeCurve;
    [SerializeField] private float _fadeTime;
    [SerializeField] private bool _fadeIn;

    private TextMeshProUGUI _textMesh;
    private Utils.Timer _timer;

    public bool Completed => _timer.HasPassed();

    private void Awake()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        if (_fadeIn)
        {
            _textMesh.alpha = 0f;
        }
    }

    private void OnEnable()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        if (_fadeIn)
        {
            _textMesh.alpha = 0f;
        }
    }

    private void Update()
    {
        if (_timer == null) return;

        if(_timer.HasPassed())
        {
            _textMesh.alpha = _fadeIn ? 1f : 0f;
            Destroy(this);
            return;
        }
        float currentValue = _fadeCurve.Evaluate(_timer.passedValue());

        _textMesh.alpha = _fadeIn ? currentValue : 1f - currentValue;
    }

    public void TriggerFade()
    {
        _timer = new Utils.Timer(_fadeTime);
    }
}
